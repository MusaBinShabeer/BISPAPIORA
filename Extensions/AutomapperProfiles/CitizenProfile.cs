using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankDTO;
using BISPAPIORA.Models.DTOS.EmploymentDTO;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.RegistrationDTO;
using BISPAPIORA.Models.ENUMS;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class CitizenProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public CitizenProfile()
        {
            #region Registered Citizen
            CreateMap<AddRegistrationDTO, tbl_citizen>()
             .ForMember(d => d.citizen_name, opt => opt.MapFrom(src => src.citizenName))
             .ForMember(d => d.citizen_phone_no, opt => opt.MapFrom(src => src.citizenPhoneNo))
             .ForMember(d => d.citizen_gender, opt => opt.MapFrom(src => $"{(GenderEnum)src.citizenGender}"))
             .ForMember(d => d.citizen_address, opt => opt.MapFrom(src => src.citizenAddress))
             .ForMember(d => d.fk_tehsil, opt => opt.MapFrom(src => Guid.Parse(src.fkTehsil)))
             .ForMember(d => d.fk_citizen_employment, opt => opt.MapFrom(src => Guid.Parse(src.fkEmployment)))
             .ForMember(d => d.fk_citizen_education, opt => opt.MapFrom(src => Guid.Parse(src.fkEducation)))
             .ForMember(d => d.citizen_cnic, opt => opt.MapFrom(src => src.citizenCnic));
            CreateMap<UpdateRegistrationDTO, tbl_citizen>()
             .ForMember(d => d.citizen_id, opt => opt.MapFrom((src, dest) => dest.citizen_id))
             .ForMember(d => d.citizen_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenName) ? src.citizenName : dest.citizen_name))
             .ForMember(d => d.citizen_phone_no, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenPhoneNo) ? src.citizenPhoneNo : dest.citizen_phone_no))
             .ForMember(d => d.citizen_gender, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenGender) ? $"{(GenderEnum)src.citizenGender}" : dest.citizen_gender))
             .ForMember(d => d.citizen_address, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenAddress) ? src.citizenAddress : dest.citizen_address))
             .ForMember(d => d.fk_tehsil, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkTehsil) ? Guid.Parse(src.fkTehsil) : dest.fk_tehsil))
             .ForMember(d => d.fk_citizen_education, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkEducation) ? Guid.Parse(src.fkEducation) : dest.fk_citizen_education))
             .ForMember(d => d.tbl_citizen_employment, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkEmployment) ? Guid.Parse(src.fkEmployment) : dest.fk_citizen_employment))
             .ForMember(d => d.citizen_cnic, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenCnic) ? src.citizenCnic : dest.citizen_cnic));
            CreateMap<tbl_registration, RegistrationResponseDTO>()
             .ForMember(d => d.registrationId, opt => opt.MapFrom(src => src.registration_id))
             .ForMember(d => d.citizenId, opt => opt.MapFrom(src => src.tbl_citizen.citizen_id))
             .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_name))
             .ForMember(d => d.citizenPhoneNo, opt => opt.MapFrom(src => src.tbl_citizen.citizen_phone_no))
             .ForMember(d => d.citizenGender, opt => opt.MapFrom(src => (GenderEnum)Enum.Parse(typeof(GenderEnum), src.tbl_citizen.citizen_gender)))
             .ForMember(d => d.genderName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_gender))
             .ForMember(d => d.citizenAddress, opt => opt.MapFrom(src => src.tbl_citizen.citizen_address))
             .ForMember(d => d.fkTehsil, opt => opt.MapFrom(src => src.tbl_citizen.fk_tehsil))
             .ForMember(d => d.tehsilName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_tehsil != null ? src.tbl_citizen.tbl_citizen_tehsil.tehsil_name : ""))
             .ForMember(d => d.districtName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_tehsil != null ? src.tbl_citizen.tbl_citizen_tehsil.tbl_district.district_name : ""))
             .ForMember(d => d.provinceName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_tehsil != null ? src.tbl_citizen.tbl_citizen_tehsil.tbl_district.tbl_province.province_name : ""))
             .ForMember(d => d.tehsilName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_tehsil != null ? src.tbl_citizen.tbl_citizen_tehsil.tehsil_name : ""))
             .ForMember(d => d.fkEmployment, opt => opt.MapFrom(src => src.tbl_citizen.fk_citizen_employment))
             .ForMember(d => d.employmentName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_employment != null ? src.tbl_citizen.tbl_citizen_employment.employment_name : ""))
             .ForMember(d => d.fkEducation, opt => opt.MapFrom(src => src.tbl_citizen.fk_citizen_education))
             .ForMember(d => d.educationName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_education != null ? src.tbl_citizen.tbl_citizen_education.education_name : ""))
             .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.tbl_citizen.citizen_cnic))
             .ForMember(d => d.ibanNo, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_bank_info.iban_no))
             .ForMember(d => d.accountHolderName, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_bank_info.account_holder_name))
             .ForMember(d => d.aIOA, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_bank_info.a_i_o_f))
             .ForMember(d => d.familySavingAccount, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_bank_info.family_saving_account))
             .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => (src.tbl_citizen.tbl_citizen_bank_info.fk_citizen)))
             .ForMember(d => d.citizenName, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_bank_info.tbl_citizen.citizen_name))
             .ForMember(d => d.fkBank, opt => opt.MapFrom(src => (src.tbl_citizen.tbl_citizen_bank_info.fk_bank)))
             .ForMember(d => d.bankName, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_bank_info.tbl_bank.bank_name));
            CreateMap<tbl_citizen, RegistrationResponseDTO>()
            .ForMember(d => d.citizenId, opt => opt.MapFrom(src => src.citizen_id))
            .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.citizen_name))
            .ForMember(d => d.citizenPhoneNo, opt => opt.MapFrom(src => src.citizen_phone_no))
            .ForMember(d => d.citizenGender, opt => opt.MapFrom(src => (GenderEnum)Enum.Parse(typeof(GenderEnum), src.citizen_gender)))
            .ForMember(d => d.genderName, opt => opt.MapFrom(src => src.citizen_gender))
            .ForMember(d => d.citizenAddress, opt => opt.MapFrom(src => src.citizen_address))
            .ForMember(d => d.fkTehsil, opt => opt.MapFrom(src => src.fk_tehsil))
            .ForMember(d => d.tehsilName, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tehsil_name : ""))
            .ForMember(d => d.districtName, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tbl_district.district_name : ""))
            .ForMember(d => d.provinceName, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tbl_district.tbl_province.province_name : ""))
            .ForMember(d => d.fkEmployment, opt => opt.MapFrom(src => src.fk_citizen_employment))
            .ForMember(d => d.employmentName, opt => opt.MapFrom(src => src.tbl_citizen_employment != null ? src.tbl_citizen_employment.employment_name : ""))
            .ForMember(d => d.fkEducation, opt => opt.MapFrom(src => src.fk_citizen_education))
            .ForMember(d => d.educationName, opt => opt.MapFrom(src => src.tbl_citizen_education != null ? src.tbl_citizen_education.education_name : ""))
            .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.citizen_cnic))
            .ForMember(d => d.ibanNo, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.iban_no))
            .ForMember(d => d.accountHolderName, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.account_holder_name))
            .ForMember(d => d.aIOA, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.a_i_o_f))
            .ForMember(d => d.familySavingAccount, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.family_saving_account))
            .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => (src.tbl_citizen_bank_info.fk_citizen)))
            .ForMember(d => d.citizenName, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.tbl_citizen.citizen_name))
            .ForMember(d => d.fkBank, opt => opt.MapFrom(src => (src.tbl_citizen_bank_info.fk_bank)))
            .ForMember(d => d.bankName, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.tbl_bank.bank_name));
            #endregion
            #region Enrolled Citizen
            CreateMap<AddEnrollmentDTO, tbl_citizen>()
             .ForMember(d => d.citizen_name, opt => opt.MapFrom(src => src.citizenName))
             .ForMember(d => d.citizen_phone_no, opt => opt.MapFrom(src => src.citizenPhoneNo))
             .ForMember(d => d.citizen_gender, opt => opt.MapFrom(src => $"{(GenderEnum)src.citizenGender}"))
             .ForMember(d => d.citizen_martial_status, opt => opt.MapFrom(src => $"{(MartialStatusEnum)src.martialStatus}"))
             .ForMember(d => d.citizen_address, opt => opt.MapFrom(src => src.citizenAddress))
             .ForMember(d => d.citizen_father_spouce_name, opt => opt.MapFrom(src => src.fatherSpouseName))
             .ForMember(d => d.citizen_date_of_birth, opt => opt.MapFrom((src, dest) => otherServices.Check(src.dateOfBirth) ? DateTime.Parse(src.dateOfBirth) : dest.citizen_date_of_birth))
             .ForMember(d => d.citizen_cnic, opt => opt.MapFrom(src => src.citizenCnic));
            CreateMap<UpdateEnrollmentDTO, tbl_citizen>()
             .ForMember(d => d.citizen_id, opt => opt.MapFrom((src, dest) => dest.citizen_id))
             .ForMember(d => d.citizen_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenName) ? src.citizenName : dest.citizen_name))
             .ForMember(d => d.citizen_phone_no, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenPhoneNo) ? src.citizenPhoneNo : dest.citizen_phone_no))
             .ForMember(d => d.citizen_martial_status, opt => opt.MapFrom((src, dest) => otherServices.Check(src.martialStatus) ? $"{(GenderEnum)src.martialStatus}" : dest.citizen_martial_status))
             .ForMember(d => d.citizen_address, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenAddress) ? src.citizenAddress : dest.citizen_address))
             .ForMember(d => d.citizen_father_spouce_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fatherSpouseName) ? src.fatherSpouseName : dest.citizen_father_spouce_name))
             .ForMember(d => d.citizen_date_of_birth, opt => opt.MapFrom((src, dest) => otherServices.Check(src.dateOfBirth) ? DateTime.Parse(src.dateOfBirth) : dest.citizen_date_of_birth))
             .ForMember(d => d.citizen_cnic, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenCnic) ? src.citizenCnic : dest.citizen_cnic));
            CreateMap<tbl_enrollment, EnrollmentResponseDTO>()
             .ForMember(d => d.enrollmentId, opt => opt.MapFrom(src => src.enrollment_id))
             .ForMember(d => d.citizenId, opt => opt.MapFrom(src => src.tbl_citizen.citizen_id))
             .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_name))
             .ForMember(d => d.fatherSpouseName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_name))
             .ForMember(d => d.citizenPhoneNo, opt => opt.MapFrom(src => src.tbl_citizen.citizen_father_spouce_name))
             .ForMember(d => d.martialStatus, opt => opt.MapFrom(src => (MartialStatusEnum)Enum.Parse(typeof(MartialStatusEnum), src.tbl_citizen.citizen_martial_status)))
             .ForMember(d => d.citizenAddress, opt => opt.MapFrom(src => src.tbl_citizen.citizen_address))
             .ForMember(d => d.dateOfBirth, opt => opt.MapFrom(src => src.tbl_citizen.citizen_date_of_birth))
            .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.tbl_citizen.citizen_cnic));
            CreateMap<tbl_citizen, EnrollmentResponseDTO>()
             .ForMember(d => d.citizenId, opt => opt.MapFrom(src => src.citizen_id))
             .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.citizen_name))
             .ForMember(d => d.fatherSpouseName, opt => opt.MapFrom(src => src.citizen_name))
             .ForMember(d => d.citizenPhoneNo, opt => opt.MapFrom(src => src.citizen_father_spouce_name))
             .ForMember(d => d.martialStatus, opt => opt.MapFrom(src => (MartialStatusEnum)Enum.Parse(typeof(MartialStatusEnum), src.citizen_martial_status)))
             .ForMember(d => d.citizenAddress, opt => opt.MapFrom(src => src.citizen_address))
             .ForMember(d => d.dateOfBirth, opt => opt.MapFrom(src => src.citizen_date_of_birth))
            .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.citizen_cnic))
            .ForMember(d => d.ibanNo, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.iban_no))
            .ForMember(d => d.accountTypeName, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.account_type))
            .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => (src.tbl_citizen_bank_info.fk_citizen)))
            .ForMember(d => d.citizenName, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.tbl_citizen.citizen_name))
            .ForMember(d => d.BankName, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.tbl_bank.bank_name))
            .ForMember(d => d.citizenSchemeYear, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_year))
            .ForMember(d => d.citizenSchemeQuarter, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_quarter))
            .ForMember(d => d.citizenSchemeStartingMonth, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_starting_month))
            .ForMember(d => d.citizenSchemeSavingAmount, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_saving_amount));
            #endregion
        }
    }
}
