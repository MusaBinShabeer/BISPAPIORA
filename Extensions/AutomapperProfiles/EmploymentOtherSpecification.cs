using AutoMapper;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO;
using BISPAPIORA.Models.DTOS.EmploymentOtherSpecificationDTO;
using BISPAPIORA.Models.DTOS.RegistrationDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class EmploymentOtherSpecification:Profile
    {
        private readonly OtherServices otherServices = new();
        public EmploymentOtherSpecification() 
        {
            CreateMap<AddRegistrationDTO, AddEmploymentOtherSpecificationDTO>()
           .ForMember(d => d.employmentOtherSpecification, opt => opt.MapFrom(src => src.citizenEmploymentOtherSpecification))
           .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fkCitizen));
            CreateMap<AddEmploymentOtherSpecificationDTO, tbl_employment_other_specification>()
             .ForMember(d => d.employment_other_specification, opt => opt.MapFrom(src => src.employmentOtherSpecification))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)));
            CreateMap<UpdateEmploymentOtherSpecificationDTO, tbl_employment_other_specification>()
             .ForMember(d => d.employment_other_specification_id, opt => opt.MapFrom((src, dest) => dest.employment_other_specification_id))
             .ForMember(d => d.employment_other_specification, opt => opt.MapFrom((src, dest) => otherServices.Check(src.employmentOtherSpecification) ? src.employmentOtherSpecification : dest.employment_other_specification))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen));
            CreateMap<tbl_employment_other_specification, EmploymentOtherSpecificationResponseDTO>()
             .ForMember(d => d.employmentOtherSpecificationId, opt => opt.MapFrom(src => src.employment_other_specification_id))
             .ForMember(d => d.employmentOtherSpecification, opt => opt.MapFrom((src) => src.employment_other_specification))
             .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fk_citizen));
        }
    }
}
