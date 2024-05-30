using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.RegistrationDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class BankOtherSpecificationProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public BankOtherSpecificationProfile()
        {
            CreateMap<(AddRegistrationDTO reg, RegisteredCitizenBankInfoResponseDTO dto), AddRegisteredBankOtherSpecificationDTO>()
            .ForMember(d => d.bankOtherSpecification, opt => opt.MapFrom(src => src.reg.citizenBankOtherSpecification))
            .ForMember(d => d.fkCitizenFamilyBankInfo, opt => opt.MapFrom(src => (src.dto.CitizenBankInfoId)));
            CreateMap<AddRegisteredBankOtherSpecificationDTO, tbl_bank_other_specification>()
             .ForMember(d => d.bank_other_specification, opt => opt.MapFrom(src => src.bankOtherSpecification))
             .ForMember(d => d.fk_citizen_family_bank_info, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizenFamilyBankInfo)));
            CreateMap<(AddEnrollmentDTO reg, EnrolledCitizenBankInfoResponseDTO dto), AddEnrolledBankOtherSpecificationDTO>()
           .ForMember(d => d.bankOtherSpecification, opt => opt.MapFrom(src => src.reg.citizenBankOtherSpecification))
           .ForMember(d => d.fkCitizenBankInfo, opt => opt.MapFrom(src => src.dto.CitizenBankInfoId));
            CreateMap<AddEnrolledBankOtherSpecificationDTO, tbl_bank_other_specification>()
             .ForMember(d => d.bank_other_specification, opt => opt.MapFrom(src => src.bankOtherSpecification))
             .ForMember(d => d.fk_citizen_bank_info, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizenBankInfo)));
            CreateMap<UpdateRegisteredBankOtherSpecificationDTO, tbl_bank_other_specification>()
             .ForMember(d => d.bank_other_specification_id, opt => opt.MapFrom((src, dest) => dest.bank_other_specification_id))
             .ForMember(d => d.bank_other_specification, opt => opt.MapFrom((src, dest) => otherServices.Check(src.bankOtherSpecification) ? src.bankOtherSpecification : dest.bank_other_specification))
             .ForMember(d => d.fk_citizen_family_bank_info, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizenFamilyBankInfo) ? Guid.Parse(src.fkCitizenFamilyBankInfo) : dest.fk_citizen_family_bank_info));
            CreateMap<tbl_bank_other_specification, BankRegisteredOtherSpecificationResponseDTO>()
             .ForMember(d => d.bankOtherSpecificationId, opt => opt.MapFrom(src => src.bank_other_specification_id))
             .ForMember(d => d.bankOtherSpecification, opt => opt.MapFrom((src) => src.bank_other_specification))
             .ForMember(d => d.fkCitizenFamilyBankInfo, opt => opt.MapFrom(src => src.fk_citizen_family_bank_info));
        }
    }
}
