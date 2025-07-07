using AutoMapper;
using Data.DTO;
using Data.Entities.DatabaseDB;


namespace API_Template.Profiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            // Mapping configuration for MockEntity to MockEntityDTO
            CreateMap<MockEntity, MockEntityDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Related.Description));
        }
    }
}
