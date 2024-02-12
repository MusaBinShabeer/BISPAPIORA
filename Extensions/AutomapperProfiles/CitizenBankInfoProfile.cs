using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;
using BISPAPIORA.Models.DTOS.EducationDTO;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.RegistrationDTO;
using BISPAPIORA.Models.ENUMS;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class CitizenBankInfoProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public CitizenBankInfoProfile()
        {
            #region Registered Citizen Bank Info DTO
            CreateMap<AddRegistrationDTO, AddRegisteredCitizenBankInfoDTO>()
            .ForMember(d => d.ibanNo, opt => opt.MapFrom(src => (src.ibanNo)))
            .ForMember(d => d.accountHolderName, opt => opt.MapFrom(src => src.accountHolderName))
            .ForMember(d => d.aIOA, opt => opt.MapFrom(src => src.aIOA))
            .ForMember(d => d.familySavingAccount, opt => opt.MapFrom(src => src.familySavingAccount))
            .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fkCitizen))
            .ForMember(d => d.fkBank, opt => opt.MapFrom(src => src.fkBank));
            CreateMap<UpdateRegistrationDTO, AddRegisteredCitizenBankInfoDTO>()
         .ForMember(d => d.ibanNo, opt => opt.MapFrom(src => (src.ibanNo)))
         .ForMember(d => d.accountHolderName, opt => opt.MapFrom(src => src.accountHolderName))
         .ForMember(d => d.aIOA, opt => opt.MapFrom(src => src.aIOA))
         .ForMember(d => d.familySavingAccount, opt => opt.MapFrom(src => src.familySavingAccount))
         .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fkCitizen))
         .ForMember(d => d.fkBank, opt => opt.MapFrom(src => src.fkBank));
            CreateMap<AddRegisteredCitizenBankInfoDTO, tbl_citizen_bank_info>()
            .ForMember(d => d.iban_no, opt => opt.MapFrom(src => (src.ibanNo)))
            .ForMember(d => d.account_holder_name, opt => opt.MapFrom(src => src.accountHolderName))
            .ForMember(d => d.a_i_o_f, opt => opt.MapFrom(src => src.aIOA))
            .ForMember(d => d.family_saving_account, opt => opt.MapFrom(src => src.familySavingAccount))
            .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => src.fkCitizen))
            .ForMember(d => d.fk_bank, opt => opt.MapFrom(src => src.fkBank));
            CreateMap<UpdateRegisteredCitizenBankInfoDTO, tbl_citizen_bank_info>()
            .ForMember(d => d.citizen_bank_info_id, opt => opt.MapFrom((src, dest) => (dest.citizen_bank_info_id)))
            .ForMember(d => d.iban_no, opt => opt.MapFrom((src, dest) => otherServices.Check(src.ibanNo) ? src.ibanNo : dest.iban_no))
            .ForMember(d => d.account_holder_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.accountHolderName) ? src.accountHolderName : dest.account_holder_name))
            .ForMember(d => d.a_i_o_f, opt => opt.MapFrom((src, dest) => otherServices.Check(src.aIOA) ? decimal.Parse(src.aIOA.ToString()) : dest.a_i_o_f))
            .ForMember(d => d.family_saving_account, opt => opt.MapFrom((src, dest) => otherServices.Check(src.familySavingAccount) ? src.familySavingAccount : dest.family_saving_account))
            .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen))
            .ForMember(d => d.fk_bank, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkBank) ? Guid.Parse(src.fkBank) : dest.fk_bank));
            CreateMap<tbl_citizen_bank_info, RegisteredCitizenBankInfoResponseDTO>()
            .ForMember(d => d.CitizenBankInfoId, opt => opt.MapFrom(src => (src.citizen_bank_info_id)))
            .ForMember(d => d.ibanNo, opt => opt.MapFrom((src) => src.iban_no))
            .ForMember(d => d.accountHolderName, opt => opt.MapFrom((src) => src.account_holder_name))
            .ForMember(d => d.aIOA, opt => opt.MapFrom((src) => src.a_i_o_f))
            .ForMember(d => d.familySavingAccount, opt => opt.MapFrom((src) => src.family_saving_account))
            .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => (src.fk_citizen)))
            .ForMember(d => d.citizenName, opt => opt.MapFrom((src) => src.tbl_citizen.citizen_name))
            .ForMember(d => d.fkBank, opt => opt.MapFrom(src => (src.fk_bank)))
            .ForMember(d => d.BankName, opt => opt.MapFrom((src) => src.tbl_bank.bank_name));
          
            #endregion
            #region Enrolled Citizen Bank Info DTO
            CreateMap<AddEnrolledCitizenBankInfoDTO, tbl_citizen_bank_info>()
            .ForMember(d => d.iban_no, opt => opt.MapFrom(src => src.ibanNo))
            .ForMember(d => d.account_type, opt => opt.MapFrom(src => $"{(AccountTypeEnum)src.accountType}"))
            .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => src.fkCitizen))
            .ForMember(d => d.fk_bank, opt => opt.MapFrom(src => src.fkBank));
            CreateMap<AddEnrollmentDTO, AddEnrolledCitizenBankInfoDTO>()
           .ForMember(d => d.ibanNo, opt => opt.MapFrom(src => src.ibanNo))
           .ForMember(d => d.accountType, opt => opt.MapFrom(src => src.accountType))
           .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fkCitizen))
           .ForMember(d => d.fkBank, opt => opt.MapFrom(src => src.fkBank));
            CreateMap<AddEnrolledCitizenBankInfoDTO, UpdateEnrolledCitizenBankInfoDTO>()
         .ForMember(d => d.ibanNo, opt => opt.MapFrom(src => src.ibanNo))
         .ForMember(d => d.accountType, opt => opt.MapFrom(src => src.accountType))
         .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fkCitizen))
         .ForMember(d => d.fkBank, opt => opt.MapFrom(src => src.fkBank));
            CreateMap<UpdateEnrolledCitizenBankInfoDTO, tbl_citizen_bank_info>()
            .ForMember(d => d.citizen_bank_info_id, opt => opt.MapFrom((src, dest) => (dest.citizen_bank_info_id)))
            .ForMember(d => d.iban_no, opt => opt.MapFrom((src, dest) => otherServices.Check(src.ibanNo) ? src.ibanNo : dest.iban_no))
            .ForMember(d => d.account_type, opt => opt.MapFrom((src, dest) => otherServices.Check(src.accountType) ? $"{(AccountTypeEnum)src.accountType}" : dest.account_type))
            .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen))
            .ForMember(d => d.fk_bank, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkBank) ? Guid.Parse(src.fkBank) : dest.fk_bank));
            CreateMap<tbl_citizen_bank_info, EnrolledCitizenBankInfoResponseDTO>()
            .ForMember(d => d.CitizenBankInfoId, opt => opt.MapFrom(src => src.citizen_bank_info_id))
            .ForMember(d => d.ibanNo, opt => opt.MapFrom((src) => src.iban_no))
            .ForMember(d => d.accountType, opt => opt.MapFrom((src) => (AccountTypeEnum)Enum.Parse(typeof(AccountTypeEnum), src.account_type)))
            .ForMember(d => d.accountTypeName, opt => opt.MapFrom((src) =>  src.account_type))
            .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => (src.fk_citizen)))
            .ForMember(d => d.citizenName, opt => opt.MapFrom((src) => src.tbl_citizen.citizen_name))
            .ForMember(d => d.fkBank, opt => opt.MapFrom(src => (src.fk_bank)))
            .ForMember(d => d.BankName, opt => opt.MapFrom((src) => src.tbl_bank.bank_name));
            #endregion
        }
    }
}
