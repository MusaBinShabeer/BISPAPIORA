using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.EmploymentDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class EmploymentProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public EmploymentProfile()
        {
            CreateMap<AddEmploymentDTO, tbl_employment>()
             .ForMember(d => d.employment_name, opt => opt.MapFrom(src => src.employmentName))
             .ForMember(d => d.is_active, opt => opt.MapFrom(src => src.isActive));
            CreateMap<UpdateEmploymentDTO, tbl_employment>()
             .ForMember(d => d.employment_id, opt => opt.MapFrom((src, dest) => dest.employment_id))
             .ForMember(d => d.employment_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.employmentName) ? src.employmentName : dest.employment_name))
             .ForMember(d => d.is_active, opt => opt.MapFrom((src, dest) => src.isActive));
            CreateMap<tbl_employment, EmploymentResponseDTO>()
            .ForMember(d => d.employmentId, opt => opt.MapFrom(src => src.employment_id))
            .ForMember(d => d.employmentCode, opt => opt.MapFrom(src => src.code))
            .ForMember(d => d.employmentName, opt => opt.MapFrom((src) => src.employment_name))
            .ForMember(d => d.isActive, opt => opt.MapFrom(src => src.is_active));
        }
    }
}
