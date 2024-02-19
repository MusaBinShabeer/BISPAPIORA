using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.EducationDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{

    public class EducationProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public EducationProfile()
        {
            CreateMap<AddEducationDTO, tbl_education>()
             .ForMember(d => d.education_name, opt => opt.MapFrom(src => src.educationName))
             .ForMember(d => d.is_active, opt => opt.MapFrom(src => src.isActive));
            CreateMap<UpdateEducationDTO, tbl_education>()
             .ForMember(d => d.education_id, opt => opt.MapFrom((src, dest) => dest.education_id))
             .ForMember(d => d.education_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.educationName) ? src.educationName : dest.education_name))
             .ForMember(d => d.is_active, opt => opt.MapFrom((src, dest) => src.isActive));
            CreateMap<tbl_education, EducationResponseDTO>()
            .ForMember(d => d.educationId, opt => opt.MapFrom(src => src.education_id))
            .ForMember(d => d.educationCode, opt => opt.MapFrom(src => src.code))
            .ForMember(d => d.educationName, opt => opt.MapFrom((src) => src.education_name))
            .ForMember(d => d.isActive, opt => opt.MapFrom(src => src.is_active));
        }
    }
}
