using ARMApocalypseSASAPI.Dtos;
using ARMApocalypseSASAPI.Models;
using AutoMapper;

namespace ARMApocalypseSASAPI.Mappers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {

            CreateMap<Item, ItemResponse>()
                .ForMember(x => x.ItemId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price))      
                
                .ReverseMap();

            CreateMap<Survivor, SurvivorResponse>()
                .ForMember(x => x.SurvivorId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Age, opt => opt.MapFrom(x => x.Age))
                .ForMember(x => x.LastLocationLongitude, opt => opt.MapFrom(x => x.LastLocationLongitude))
                .ForMember(x => x.LastLocationLatitude, opt => opt.MapFrom(x => x.LastLocationLatitude))
                .ForMember(x => x.Gender, opt => opt.MapFrom(x => x.Gender))
                .ReverseMap();


            CreateMap<TradeItem, TradeItemResponse>()

                .ForMember(x => x.TradeItemId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Quantity, opt => opt.MapFrom(x => x.Quantity))
                .ForMember(x => x.UnitPoints, opt => opt.MapFrom(x => x.UnitPoints))
                .ReverseMap();




        }
    }
}
