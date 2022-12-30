using ARMApocalypseSASAPI.Dtos;
using ARMApocalypseSASAPI.Interfaces;
using ARMApocalypseSASAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMApocalypseSASAPI.Tests
{
    public class CoreServiceMock : ICoreService
    {
        private readonly List<Survivor> _survivors;
        private readonly List<Item> _items;

        public CoreServiceMock()
        {
            _survivors = new List<Survivor>()
            {
                new Survivor()
                {
                    Id = Guid.NewGuid().ToString(), Name = "Survivor 001", IsDeleted = false, IsActive = true,
                },
               new Survivor()
                {
                    Id = Guid.NewGuid().ToString(), Name = "Survivor 002", IsDeleted = false, IsActive = true,
                },
                new Survivor()
                {
                    Id = Guid.NewGuid().ToString(), Name = "Survivor 003", IsDeleted = false, IsActive = true,
                },
            };
            _items = new List<Item>()
            {
                new Item{Id = Guid.NewGuid().ToString(), Name = "Water", IsActive = true, IsDeleted = false, Price = 4},
                new Item{Id = Guid.NewGuid().ToString(), Name = "Food", IsActive = true, IsDeleted = false, Price = 3},
                new Item{Id = Guid.NewGuid().ToString(), Name = "Medication", IsActive = true, IsDeleted = false, Price = 2},
                new Item{Id = Guid.NewGuid().ToString(), Name = "Ammunition", IsActive = true, IsDeleted = false, Price = 1},

            };
        }

        public Task<GenericResponse<ReportResponse>> FetchReport()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<SurvivorResponse>> FlagSurvivorAsInfected(FlagSurvivorInfectedRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponse<IEnumerable<ItemResponse>>> GetAllItems()
        {
            var items = _items.Select(x => new ItemResponse
            {
                ItemId = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,
                Price = x.Price,
                UpdatedAt = x.UpdatedAt,
            }).ToList();
            return new GenericResponse<IEnumerable<ItemResponse>>
            {
                Data = items,
                Message = "Test message",
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
        //private SurvivorResponse GenerateSurvivorResponse(Survivor newSurvivor)
        //{
        //    var response = _mapper.Map<SurvivorResponse>(newSurvivor);
        //    response.OwnTradeItems = _mapper.Map<IList<TradeItemResponse>>(newSurvivor.OwnTradeItems);

        //    return response;
        //}
        public async Task<GenericResponse<SurvivorResponse>> RegisterSurvivor(RegisterSurvivorRequest request)
        {
            var newSurvivor = new Survivor
            {
                Age = request.Age,
                IsActive = true,
                IsDeleted = false,
                Gender = request.Gender,
                LastLocationLatitude = request.LastLocationLatitude,
                LastLocationLongitude = request.LastLocationLongitude,
                Name = request.Name,
                Id = Guid.NewGuid().ToString(), 

            };

            _survivors.Add(newSurvivor);
            return new GenericResponse<SurvivorResponse>
            {
                Data = new SurvivorResponse { Name = newSurvivor.Name, LastLocationLatitude =  newSurvivor.LastLocationLatitude, 
                SurvivorId = newSurvivor.Id,
                Age = newSurvivor.Age,
                Gender = newSurvivor.Gender,
                LastLocationLongitude = newSurvivor.LastLocationLongitude,
                IsActive = newSurvivor.IsActive,
                IsDeleted = newSurvivor.IsDeleted,
                CreatedAt = newSurvivor.CreatedAt,
                UpdatedAt = newSurvivor.UpdatedAt,
                


                },
            };

        }

        public Task<GenericResponse<SurvivorInfectionReportResponse>> ReportSurvivorInfectionStatus(UpdateSurvivorInfectionStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<object>> Trade(TradingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<SurvivorResponse>> UpdateSurvivorLocation(UpdateSurvivorLocationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
