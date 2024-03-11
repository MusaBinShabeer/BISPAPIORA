using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.FunctionalityDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class FunctionalityProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public FunctionalityProfile()
        {
            CreateMap<AddFunctionalityDTO, tbl_functionality>()
             .ForMember(d => d.functionality_name, opt => opt.MapFrom(src => src.functionalityName))
             .ForMember(d => d.is_active, opt => opt.MapFrom(src => src.isActive));
            CreateMap<UpdateFunctionalityDTO, tbl_functionality>()
             .ForMember(d => d.functionality_id, opt => opt.MapFrom((src, dest) => dest.functionality_id))
             .ForMember(d => d.functionality_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.functionalityName) ? src.functionalityName : dest.functionality_name))
             .ForMember(d => d.is_active, opt => opt.MapFrom((src, dest) => src.isActive));
            CreateMap<tbl_functionality, FunctionalityResponseDTO>()
             .ForMember(d => d.functionalityId, opt => opt.MapFrom(src => src.functionality_id))
             .ForMember(d => d.functionalityName, opt => opt.MapFrom((src) => src.functionality_name))
             .ForMember(d => d.isActive, opt => opt.MapFrom(src => src.is_active));
        }
    }
}
