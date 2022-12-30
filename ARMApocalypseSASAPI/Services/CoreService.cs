using ARMApocalypseSASAPI.Data;
using ARMApocalypseSASAPI.Dtos;
using ARMApocalypseSASAPI.Helpers;
using ARMApocalypseSASAPI.Interfaces;
using ARMApocalypseSASAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ARMApocalypseSASAPI.Services
{
    public class CoreService : ICoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApiConfig _config;

        public CoreService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<ApiConfig> config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config.Value;
        }

        public async Task<GenericResponse<IEnumerable<ItemResponse>>> GetAllItems()
        {
            try
            {
                var items = await _unitOfWork.ItemRepository.GetAsync(filter: x => x.IsActive == true);
                var itemsResponse = _mapper.Map<IEnumerable<ItemResponse>>(items);

                return new GenericResponse<IEnumerable<ItemResponse>>
                {
                    Success = true,
                    Message = items.Count() > 0 ? $"Retrieved {items.Count()} items from the database" : "No items in the database at this moment",
                    Data = itemsResponse,
                    StatusCode = System.Net.HttpStatusCode.OK,


                };
            }
            catch (Exception ex)
            {
                //log error here
                Console.WriteLine(ex);
                return new GenericResponse<IEnumerable<ItemResponse>>
                {
                    Data = Enumerable.Empty<ItemResponse>(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<GenericResponse<SurvivorResponse>> RegisterSurvivor(RegisterSurvivorRequest request)
        {
            _unitOfWork.CreateTransaction();
            try
            {

                var survivorProperties = new List<TradeItem>();
                var survivorId = Guid.NewGuid().ToString();
                var newSurvivor = _mapper.Map<Survivor>(request);
                newSurvivor.Id = survivorId;
                newSurvivor.IsActive = true;
                newSurvivor.IsInfected = false;

                if (request.OwnTradeItems.Any())
                {
                    foreach (var item in request.OwnTradeItems)
                    {
                        var globalItem = await _unitOfWork.ItemRepository.FirstOrDefaultAsync(x => x.Id == item.ItemId);
                        var ownItem = _mapper.Map<TradeItem>(item);

                        ownItem.SurvivorId = survivorId;
                        ownItem.Survivor = newSurvivor;
                        ownItem.ItemId = globalItem.Id;
                        ownItem.Item = globalItem;
                        ownItem.IsActive = true;
                        ownItem.UnitPoints = globalItem.Price;

                        ownItem.Id = Guid.NewGuid().ToString();

                        await _unitOfWork.TradeItemRepository.AddAsync(ownItem);
                        await _unitOfWork.SaveChangesAsync();

                        survivorProperties.Add(ownItem);
                    }
                }



                //create property...
                if (survivorProperties.Any())
                {
                    newSurvivor.OwnTradeItems = survivorProperties;
                    
                }

                //create survivor into DB
                await _unitOfWork.SurvivorRepository.AddAsync(newSurvivor);
                await _unitOfWork.SaveChangesAsync();

                _unitOfWork.Commit();

                var response = GenerateSurvivorResponse(newSurvivor);

                return new GenericResponse<SurvivorResponse>
                {
                    Data = response,
                    Success = true,
                    Message = "Survivor registered successfully",
                    StatusCode = System.Net.HttpStatusCode.Created
                };

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                //log error here
                Console.WriteLine(ex);
                return new GenericResponse<SurvivorResponse>
                {
                    Data = new SurvivorResponse(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message
                };
            }

        }
        private SurvivorResponse GenerateSurvivorResponse(Survivor newSurvivor)
        {
            var response = _mapper.Map<SurvivorResponse>(newSurvivor);
            response.OwnTradeItems = _mapper.Map<IList<TradeItemResponse>>(newSurvivor.OwnTradeItems);

            return response;
        }
        public async Task<GenericResponse<SurvivorInfectionReportResponse>> ReportSurvivorInfectionStatus(UpdateSurvivorInfectionStatusRequest request)
        {
            try
            {
                var reporter = await _unitOfWork.SurvivorRepository.FirstOrDefaultAsync(filter: x => x.Id == request.ReportingSurvivorID && x.IsActive && !x.IsDeleted && !x.IsInfected, include: i => i.Include(x => x.OwnTradeItems).ThenInclude(x => x.Item));
                var survivor = await _unitOfWork.SurvivorRepository.FirstOrDefaultAsync(filter: x => x.Id == request.ReportedSurvivorID && x.IsActive && !x.IsDeleted && !x.IsInfected, include: i => i.Include(x => x.OwnTradeItems).ThenInclude(x => x.Item));

                if (reporter is null)
                {
                    return new GenericResponse<SurvivorInfectionReportResponse>
                    {
                        Data = new SurvivorInfectionReportResponse(),
                        Success = false,
                        Message = "Invalid survivor",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }


                if (survivor is null)
                {
                    return new GenericResponse<SurvivorInfectionReportResponse>
                    {
                        Data = new SurvivorInfectionReportResponse(),
                        Success = false,
                        Message = "Invalid survivor",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                var newReport = _mapper.Map<SurvivorInfectionReport>(request);
                newReport.ReportingSurvivorID = reporter.Id;
                newReport.ReportedSurvivorID = survivor.Id;
                newReport.Reporter = reporter;
                newReport.ReportedSurvivor = survivor;
                newReport.Id = Guid.NewGuid().ToString();

                await _unitOfWork.SurvivorInfectionReportRepository.AddAsync(newReport);
                await _unitOfWork.SaveChangesAsync();

                var response = new SurvivorInfectionReportResponse
                {
                    ReportedSurvivor = GenerateSurvivorResponse(survivor),
                    Reporter = GenerateSurvivorResponse(reporter),
                    DateOfReport = newReport.DateOfReport,
                    Notes = newReport.Notes,
                    ReportId = newReport.Id
                };

                return new GenericResponse<SurvivorInfectionReportResponse>
                {
                    Data = response,
                    Success = true,
                    Message = "Survivor infection reported successfully",
                    StatusCode = System.Net.HttpStatusCode.Created
                };

            }
            catch (Exception ex)
            {
                //log error here
                Console.WriteLine(ex);
                return new GenericResponse<SurvivorInfectionReportResponse>
                {
                    Data = new SurvivorInfectionReportResponse(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<GenericResponse<SurvivorResponse>> UpdateSurvivorLocation(UpdateSurvivorLocationRequest request)
        {
            try
            {

                var survivor = await _unitOfWork.SurvivorRepository.FirstOrDefaultAsync(filter: x => x.Id == request.SurvivorId && x.IsActive && !x.IsDeleted && !x.IsInfected, include: i => i.Include(x => x.OwnTradeItems).ThenInclude(x => x.Item));
                if(survivor is null)
                {
                    return new GenericResponse<SurvivorResponse>
                    {
                        Data = new SurvivorResponse(),
                        Success = false,
                        Message = "Invalid survivor",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                _mapper.Map(request, survivor);
                
                _unitOfWork.SurvivorRepository.Update(survivor);
                await _unitOfWork.SaveChangesAsync();


                var response = GenerateSurvivorResponse(survivor);


                return new GenericResponse<SurvivorResponse>
                {
                    Data = response,
                    Success = true,
                    Message = "Survivor location updated successfully",
                    StatusCode = System.Net.HttpStatusCode.Created
                };

            }
            catch (Exception ex)
            {
                //log error here
                Console.WriteLine(ex);
                return new GenericResponse<SurvivorResponse>
                {
                    Data = new SurvivorResponse(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<GenericResponse<SurvivorResponse>> FlagSurvivorAsInfected(FlagSurvivorInfectedRequest request)
        {
            try
            {

                var survivor = await _unitOfWork.SurvivorRepository.FirstOrDefaultAsync(filter: x => x.Id == request.SurvivorId, include: i => i.Include(x => x.OwnTradeItems).ThenInclude(x => x.Item));
                if (survivor is null)
                {
                    return new GenericResponse<SurvivorResponse>
                    {
                        Data = new SurvivorResponse(),
                        Success = false,
                        Message = "Invalid survivor",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                var reportsYetForSurvivor = await _unitOfWork.SurvivorInfectionReportRepository.GetCountAsync(x => x.ReportedSurvivorID == survivor.Id);
                int.TryParse(_config.MaximumInfectionFlagReports, out var maximumInfectionFlagReports);
                var response = new SurvivorResponse();
                if(reportsYetForSurvivor < maximumInfectionFlagReports)
                {
                    return new GenericResponse<SurvivorResponse>
                    {
                        Data = response,
                        Success = false,
                        Message = "Survivor cannot be marked infected at this time because it has not yet been reported for maximum number of times possible.",
                        StatusCode = System.Net.HttpStatusCode.BadRequest
                    };
                }

                survivor.IsInfected = true;
                survivor.IsActive = false;
                survivor.IsDeleted = true;
                _unitOfWork.SurvivorRepository.Update(survivor);
                await _unitOfWork.SaveChangesAsync();


                response = GenerateSurvivorResponse(survivor);


                return new GenericResponse<SurvivorResponse>
                {
                    Data = response,
                    Success = true,
                    Message = "Survivor flagged infected successfully",
                    StatusCode = System.Net.HttpStatusCode.OK
                };

            }
            catch (Exception ex)
            {
                //log error here
                Console.WriteLine(ex);
                return new GenericResponse<SurvivorResponse>
                {
                    Data = new SurvivorResponse(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<GenericResponse<object>> Trade(TradingRequest request)
        {
            _unitOfWork.CreateTransaction();

            var buyerSurvivorProperties = new List<TradeItem>();
            var sellerSurvivorProperties = new List<TradeItem>();
            try
            {

                var buyer = await _unitOfWork.SurvivorRepository.FirstOrDefaultAsync(filter: x => x.Id == request.BuyerSurvivorId && x.IsActive && !x.IsDeleted && !x.IsInfected, include: i => i.Include(x => x.OwnTradeItems).ThenInclude(x => x.Item));
                var seller = await _unitOfWork.SurvivorRepository.FirstOrDefaultAsync(filter: x => x.Id == request.SellerSurvivorId && x.IsActive && !x.IsDeleted && !x.IsInfected, include: i => i.Include(x => x.OwnTradeItems).ThenInclude(x => x.Item));
                if (buyer is null)
                {
                    return new GenericResponse<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "Invalid buyer",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                if (seller is null)
                {
                    return new GenericResponse<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "Invalid seller",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                var buyerItems = request.BuyerItems.Select(x => _unitOfWork.ItemRepository.FirstOrDefault(b => b.Id == x.ItemId)).ToList();
                var sellerItems = request.SellerItems.Select(x => _unitOfWork.ItemRepository.FirstOrDefault(b => b.Id == x.ItemId)).ToList();

                var buyerTotalAskingPoints = buyerItems.Sum(x => x.Price);
                var sellerTotalAskingPoints = sellerItems.Sum(x => x.Price);
                if(buyerTotalAskingPoints != sellerTotalAskingPoints)
                {
                    return new GenericResponse<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "Left handside total points must be equal to right handside total points.",
                        StatusCode = System.Net.HttpStatusCode.BadRequest
                    };
                }

                //No conflict, the trade can go on successfully! 
                //try to swap here.... 
                if (buyerItems.Any())
                {
                    foreach (var item in buyerItems)
                    {
                        var globalItem = await _unitOfWork.ItemRepository.FirstOrDefaultAsync(x => x.Id == item.Id);
                        var ownItem = _mapper.Map<TradeItem>(item);
                        //transfer them to the seller survivor
                        ownItem.SurvivorId = seller.Id;
                        ownItem.Survivor = seller;
                        ownItem.ItemId = globalItem.Id;
                        ownItem.Item = globalItem;
                        ownItem.IsActive = true;
                        ownItem.UnitPoints = globalItem.Price;

                        ownItem.Id = Guid.NewGuid().ToString();

                        await _unitOfWork.TradeItemRepository.AddAsync(ownItem);
                        await _unitOfWork.SaveChangesAsync();

                        //transfered to seller;...
                        sellerSurvivorProperties.Add(ownItem);
                    }
                }

                if (sellerItems.Any())
                {
                    foreach (var item in sellerItems)
                    {
                        var globalItem = await _unitOfWork.ItemRepository.FirstOrDefaultAsync(x => x.Id == item.Id);
                        var ownItem = _mapper.Map<TradeItem>(item);

                        //transfer them to the buyer survivor

                        ownItem.SurvivorId = buyer.Id;
                        ownItem.Survivor = buyer;
                        ownItem.ItemId = globalItem.Id;
                        ownItem.Item = globalItem;
                        ownItem.IsActive = true;
                        ownItem.UnitPoints = globalItem.Price;
                        ownItem.Id = Guid.NewGuid().ToString();

                        await _unitOfWork.TradeItemRepository.AddAsync(ownItem);
                        await _unitOfWork.SaveChangesAsync();

                        //transfered to buyer...
                        buyerSurvivorProperties.Add(ownItem);
                    }
                }
                //create property...
                if (buyerSurvivorProperties.Any())
                {
                    buyer.OwnTradeItems.AddRange(buyerSurvivorProperties);

                }
                if (sellerSurvivorProperties.Any())
                {
                    seller.OwnTradeItems.AddRange(sellerSurvivorProperties);

                }

                //UPDATE buyer's info into DB
                _unitOfWork.SurvivorRepository.Update(buyer);
                await _unitOfWork.SaveChangesAsync();

                _unitOfWork.SurvivorRepository.Update(seller);  
                await _unitOfWork.SaveChangesAsync();

                _unitOfWork.Commit();
                return new GenericResponse<object>
                {
                    Data = null,
                    Success = true,
                    Message = "Trade facilitated successfully",
                    StatusCode = System.Net.HttpStatusCode.OK
                };

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                //log error here
                Console.WriteLine(ex);
                return new GenericResponse<object>
                {
                    Data = new SurvivorResponse(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<GenericResponse<ReportResponse>> FetchReport()
        {
            try
            {
                var totalTradeItems = await _unitOfWork.TradeItemRepository.GetAsync(x => x.IsActive, include: i => i.Include(x => x.Item));
                var totalSurvivors = await _unitOfWork.SurvivorRepository.GetCountAsync(x => x.IsActive);

                var totalInfectedSurvivors = await _unitOfWork.SurvivorRepository.GetCountAsync(x => x.IsInfected);
                var totalUnInfectedSurvivors = await _unitOfWork.SurvivorRepository.GetCountAsync(x => !x.IsInfected);

                var totalPointsLostDueToInfection = (await _unitOfWork.TradeItemRepository.GetAsync(x => x.Survivor.IsInfected, include: i => i.Include(x => x.Survivor))).Sum(x => x.UnitPoints);
                //this is complete ps.... I'm coming back to this... the logic is not right yet....
                var itemShift = totalTradeItems.Select(x => new ItemAverageResponse
                {
                    ItemName = x.Item.Name,
                    Total = totalTradeItems.Where(x => x.ItemId == x.Item.Id).Sum(x => x.UnitPoints),
                    Average = (totalTradeItems.Where(x => x.ItemId == x.Item.Id).Sum(x => x.UnitPoints) / totalTradeItems.Count(x => x.ItemId == x.Item.Id))
                }).ToList();

                //var itesmShift = totalTradeItems.Select(x => x.Item).ToList().Select(y => new ItemAverageResponse
                //{
                //    ItemName = y.Name,
                //    Average = 

                //}).ToList();
                return new GenericResponse<ReportResponse>
                {
                    Data = new ReportResponse
                    {
                        InfectedSurvivorsPercent = totalInfectedSurvivors,
                        ItemAverageResponses = itemShift,
                        NonInfectedSurvivorsPercent = totalUnInfectedSurvivors,
                        PointsLostDueToInfection = totalPointsLostDueToInfection

                    },
                    Success = true,
                    Message = "Report fetched successfully",
                    StatusCode = System.Net.HttpStatusCode.Created
                };

            }
            catch (Exception ex)
            {
                //log error here
                Console.WriteLine(ex);
                return new GenericResponse<ReportResponse>
                {
                    Data = new ReportResponse(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
