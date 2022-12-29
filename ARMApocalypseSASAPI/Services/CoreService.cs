using ARMApocalypseSASAPI.Data;
using ARMApocalypseSASAPI.Dtos;
using ARMApocalypseSASAPI.Interfaces;
using ARMApocalypseSASAPI.Models;
using AutoMapper;

namespace ARMApocalypseSASAPI.Services
{
    public class CoreService : ICoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                var reporter = await _unitOfWork.SurvivorRepository.FirstOrDefaultAsync(filter: x => x.Id == request.ReportingSurvivorID);
                var survivor = await _unitOfWork.SurvivorRepository.FirstOrDefaultAsync(filter: x => x.Id == request.ReportedSurvivorID);

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

                var survivor = await _unitOfWork.SurvivorRepository.FirstOrDefaultAsync(filter: x => x.Id == request.SurvivorId);
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
    }
}
