﻿using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankDTO;
using BISPAPIORA.Models.DTOS.CitizenDTO;
using BISPAPIORA.Models.DTOS.EmploymentDTO;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.RegistrationDTO;
using BISPAPIORA.Models.DTOS.VerificationResponseDTO;
using BISPAPIORA.Models.ENUMS;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class CitizenProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public CitizenProfile()
        {
            #region Registered Citizen
            CreateMap<(SurvayResponseDTO DTO, bool check), tbl_citizen>()
            .ForMember(d => d.unique_hh_id, opt => opt.MapFrom(src => (src.DTO.unique_hh_id)))
            .ForMember(d => d.pmt, opt => opt.MapFrom(src => (src.DTO.PMT)))
            .ForMember(d => d.is_valid_beneficiary, opt => opt.MapFrom(src =>src.check ))
            .ForMember(d => d.submission_date, opt => opt.MapFrom(src => (src.DTO.submission_date)));
            CreateMap<(SurvayResponseDTO DTO, bool check,tbl_citizen citizen), RegistrationResponseDTO>()
            .ForMember(d => d.enrollmentId, opt => opt.MapFrom(src => src.citizen.tbl_enrollment != null ? src.citizen.tbl_enrollment.enrollment_id.ToString() : ""))
            .ForMember(d => d.registrationId, opt => opt.MapFrom(src => src.citizen.tbl_citizen_registration != null ? src.citizen.tbl_citizen_registration.registration_id.ToString() : ""))
            .ForMember(d => d.citizenId, opt => opt.MapFrom(src => src.citizen.citizen_id))
            .ForMember(d => d.citizenCode, opt => opt.MapFrom(src => src.citizen.id))
            .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.citizen.citizen_name))
            .ForMember(d => d.citizenPhoneNo, opt => opt.MapFrom(src => src.citizen.citizen_phone_no))
            .ForMember(d => d.registrationDate, opt => opt.MapFrom(src => src.citizen.tbl_citizen_registration.registered_date))
            .ForMember(d => d.citizenGender, opt => opt.MapFrom(src => (GenderEnum)Enum.Parse(typeof(GenderEnum), src.citizen.citizen_gender)))
            .ForMember(d => d.genderName, opt => opt.MapFrom(src => src.citizen.citizen_gender))
            .ForMember(d => d.citizenAddress, opt => opt.MapFrom(src => src.citizen.citizen_address))
            .ForMember(d => d.fkTehsil, opt => opt.MapFrom(src => src.citizen.fk_tehsil))
            .ForMember(d => d.fkDistrict, opt => opt.MapFrom(src => src.citizen.tbl_citizen_tehsil.fk_district))
            .ForMember(d => d.fkProvince, opt => opt.MapFrom(src => src.citizen.tbl_citizen_tehsil.tbl_district.fk_province))
            .ForMember(d => d.tehsilName, opt => opt.MapFrom(src => src.citizen.tbl_citizen_tehsil != null ? src.citizen.tbl_citizen_tehsil.tehsil_name : ""))
            .ForMember(d => d.districtName, opt => opt.MapFrom(src => src.citizen.tbl_citizen_tehsil != null ? src.citizen.tbl_citizen_tehsil.tbl_district.district_name : ""))
            .ForMember(d => d.provinceName, opt => opt.MapFrom(src => src.citizen.tbl_citizen_tehsil != null ? src.citizen.tbl_citizen_tehsil.tbl_district.tbl_province.province_name : ""))
            .ForMember(d => d.fkEmployment, opt => opt.MapFrom(src => src.citizen.fk_citizen_employment))
            .ForMember(d => d.employmentName, opt => opt.MapFrom(src => src.citizen.tbl_employment_other_specification != null ? src.citizen.tbl_employment_other_specification.employment_other_specification : (src.citizen.tbl_citizen_employment != null ? src.citizen.tbl_citizen_employment.employment_name : "")))
            .ForMember(d => d.fkEducation, opt => opt.MapFrom(src => src.citizen.fk_citizen_education))
            .ForMember(d => d.educationName, opt => opt.MapFrom(src => src.citizen.tbl_citizen_education != null ? src.citizen.tbl_citizen_education.education_name : ""))
            .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.citizen.citizen_cnic))
            .ForMember(d => d.ibanNo, opt => opt.MapFrom((src) => src.citizen.tbl_citizen_family_bank_info.iban_no))
            .ForMember(d => d.accountHolderName, opt => opt.MapFrom((src) => src.citizen.tbl_citizen_family_bank_info.account_holder_name))
            .ForMember(d => d.aIOA, opt => opt.MapFrom((src) => src.citizen.tbl_citizen_family_bank_info.family_income))
            .ForMember(d => d.fkBank, opt => opt.MapFrom(src => (src.citizen.tbl_citizen_family_bank_info.fk_bank)))
            .ForMember(d => d.bankName, opt => opt.MapFrom((src) => src.citizen.tbl_citizen_family_bank_info.tbl_bank_other_specification != null ? src.citizen.tbl_citizen_family_bank_info.tbl_bank_other_specification.bank_other_specification : src.citizen.tbl_citizen_family_bank_info.tbl_bank.bank_name))
            .ForMember(d => d.uniHHId, opt => opt.MapFrom(src => (src.DTO.unique_hh_id)))
            .ForMember(d => d.pmt, opt => opt.MapFrom(src => (src.DTO.PMT)))
            .ForMember(d => d.isValidBeneficiary, opt => opt.MapFrom(src => src.check))
            .ForMember(d => d.submissionDate, opt => opt.MapFrom(src => (src.DTO.submission_date)));
            CreateMap<(SurvayResponseDTO DTO, bool check), RegistrationResponseDTO>()
              .ForMember(d => d.uniHHId, opt => opt.MapFrom(src => (src.DTO.unique_hh_id)))
              .ForMember(d => d.pmt, opt => opt.MapFrom(src => (src.DTO.PMT)))
              .ForMember(d => d.isValidBeneficiary, opt => opt.MapFrom(src => src.check))
              .ForMember(d => d.submissionDate, opt => opt.MapFrom(src => (src.DTO.submission_date)));
            CreateMap<(SurvayResponseDTO DTO, bool check), EnrollmentResponseDTO>()
               .ForMember(d => d.uniHHId, opt => opt.MapFrom(src => (src.DTO.unique_hh_id)))
               .ForMember(d => d.isValidBeneficiary, opt => opt.MapFrom(src => src.check))
               .ForMember(d => d.pmt, opt => opt.MapFrom(src => (src.DTO.PMT)))
               .ForMember(d => d.submissionDate, opt => opt.MapFrom(src => (src.DTO.submission_date)));
            CreateMap<AddRegistrationDTO, tbl_citizen>()
             .ForMember(d => d.citizen_name, opt => opt.MapFrom(src => src.citizenName))
             .ForMember(d => d.citizen_phone_no, opt => opt.MapFrom(src => src.citizenPhoneNo))
             .ForMember(d => d.citizen_gender, opt => opt.MapFrom(src => $"{(GenderEnum)src.citizenGender}"))
             .ForMember(d => d.citizen_address, opt => opt.MapFrom(src => src.citizenAddress))
             .ForMember(d => d.fk_tehsil, opt => opt.MapFrom(src => Guid.Parse(src.fkTehsil)))
             .ForMember(d => d.fk_citizen_employment, opt => opt.MapFrom(src => Guid.Parse(src.fkEmployment)))
             .ForMember(d => d.fk_citizen_education, opt => opt.MapFrom(src => Guid.Parse(src.fkEducation)))
             .ForMember(d => d.unique_hh_id, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.uniHHId) ? decimal.Parse(src.uniHHId) : default!))
             .ForMember(d => d.pmt, opt => opt.MapFrom(src => (!string.IsNullOrEmpty(src.pmt) ? src.pmt : "")))
             .ForMember(d => d.submission_date, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.submissionDate) ? DateTime.Parse(src.submissionDate) : default!))
             .ForMember(d => d.is_valid_beneficiary, opt => opt.MapFrom(src => (src.isValidBeneficiary)))
             .ForMember(d => d.citizen_cnic, opt => opt.MapFrom(src => src.citizenCnic));
            CreateMap<UpdateRegistrationDTO, tbl_citizen>()
             .ForMember(d => d.citizen_id, opt => opt.MapFrom((src, dest) => dest.citizen_id))
             .ForMember(d => d.citizen_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenName) ? src.citizenName : dest.citizen_name))
             .ForMember(d => d.citizen_phone_no, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenPhoneNo) ? src.citizenPhoneNo : dest.citizen_phone_no))
             .ForMember(d => d.citizen_gender, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenGender) ? $"{(GenderEnum)src.citizenGender}" : dest.citizen_gender))
             .ForMember(d => d.citizen_address, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenAddress) ? src.citizenAddress : dest.citizen_address))
             .ForMember(d => d.fk_tehsil, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkTehsil) ? Guid.Parse(src.fkTehsil) : dest.fk_tehsil))
             .ForMember(d => d.fk_citizen_education, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkEducation) ? Guid.Parse(src.fkEducation) : dest.fk_citizen_education))
             .ForMember(d => d.fk_citizen_employment, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkEmployment) ? Guid.Parse(src.fkEmployment) : dest.fk_citizen_employment))
             .ForMember(d => d.citizen_cnic, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenCnic) ? src.citizenCnic : dest.citizen_cnic))
             .ForMember(d => d.unique_hh_id, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.uniHHId) ? decimal.Parse(src.uniHHId) : default!))
             .ForMember(d => d.pmt, opt => opt.MapFrom(src => (!string.IsNullOrEmpty(src.pmt) ? src.pmt : "")))
             .ForMember(d => d.submission_date, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.submissionDate) ? DateTime.Parse(src.submissionDate) : default!))
             .ForMember(d => d.is_valid_beneficiary, opt => opt.MapFrom(src => (src.isValidBeneficiary)));
            CreateMap<tbl_registration, RegistrationResponseDTO>()
             .ForMember(d => d.registrationId, opt => opt.MapFrom(src => src.registration_id))
             .ForMember(d => d.citizenId, opt => opt.MapFrom(src => src.tbl_citizen.citizen_id))
             .ForMember(d => d.citizenCode, opt => opt.MapFrom(src => src.tbl_citizen.id))
             .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_name))
             .ForMember(d => d.citizenPhoneNo, opt => opt.MapFrom(src => src.tbl_citizen.citizen_phone_no))
             .ForMember(d => d.citizenGender, opt => opt.MapFrom(src => (GenderEnum)Enum.Parse(typeof(GenderEnum), src.tbl_citizen.citizen_gender)))
             .ForMember(d => d.genderName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_gender))
             .ForMember(d => d.citizenAddress, opt => opt.MapFrom(src => src.tbl_citizen.citizen_address))
             .ForMember(d => d.registrationDate, opt => opt.MapFrom(src => src.registered_date))
             .ForMember(d => d.fkTehsil, opt => opt.MapFrom(src => src.tbl_citizen.fk_tehsil))
             .ForMember(d => d.tehsilName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_tehsil != null ? src.tbl_citizen.tbl_citizen_tehsil.tehsil_name : ""))
             .ForMember(d => d.districtName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_tehsil != null ? src.tbl_citizen.tbl_citizen_tehsil.tbl_district.district_name : ""))
             .ForMember(d => d.provinceName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_tehsil != null ? src.tbl_citizen.tbl_citizen_tehsil.tbl_district.tbl_province.province_name : ""))
             .ForMember(d => d.tehsilName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_tehsil != null ? src.tbl_citizen.tbl_citizen_tehsil.tehsil_name : ""))
             .ForMember(d => d.fkEmployment, opt => opt.MapFrom(src => src.tbl_citizen.fk_citizen_employment))
             .ForMember(d => d.employmentName, opt => opt.MapFrom(src =>src.tbl_citizen.tbl_employment_other_specification!=null? src.tbl_citizen.tbl_employment_other_specification.employment_other_specification:( src.tbl_citizen.tbl_citizen_employment != null ? src.tbl_citizen.tbl_citizen_employment.employment_name : "")))
             .ForMember(d => d.fkEducation, opt => opt.MapFrom(src => src.tbl_citizen.fk_citizen_education))
             .ForMember(d => d.educationName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_education != null ? src.tbl_citizen.tbl_citizen_education.education_name : ""))
             .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.tbl_citizen.citizen_cnic))
             .ForMember(d => d.ibanNo, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_family_bank_info.iban_no))
             .ForMember(d => d.accountHolderName, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_family_bank_info.account_holder_name))
             .ForMember(d => d.aIOA, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_family_bank_info.family_income))
             .ForMember(d => d.fkBank, opt => opt.MapFrom(src => (src.tbl_citizen.tbl_citizen_family_bank_info.fk_bank)))
             .ForMember(d => d.fkRegisteredBy, opt => opt.MapFrom(src => (src.fk_registered_by)))
             .ForMember(d => d.registeredByUser, opt => opt.MapFrom(src => (src.registerd_by.user_name)))
             .ForMember(d => d.bankName, opt => opt.MapFrom((src) =>src.tbl_citizen.tbl_citizen_family_bank_info.tbl_bank_other_specification!=null?src.tbl_citizen.tbl_citizen_family_bank_info.tbl_bank_other_specification.bank_other_specification :src.tbl_citizen.tbl_citizen_family_bank_info.tbl_bank.bank_name));
            CreateMap<tbl_citizen, RegistrationResponseDTO>()
              .ForMember(d => d.enrollmentId, opt => opt.MapFrom(src => src.tbl_enrollment != null ? src.tbl_enrollment.enrollment_id.ToString() : ""))
              .ForMember(d => d.registrationId, opt => opt.MapFrom(src => src.tbl_citizen_registration != null ? src.tbl_citizen_registration.registration_id.ToString() : ""))
              .ForMember(d => d.citizenId, opt => opt.MapFrom(src => src.citizen_id))
              .ForMember(d => d.citizenCode, opt => opt.MapFrom(src => src.id))
              .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.citizen_name))
              .ForMember(d => d.citizenPhoneNo, opt => opt.MapFrom(src => src.citizen_phone_no))
              .ForMember(d => d.registrationDate, opt => opt.MapFrom(src => src.tbl_citizen_registration.registered_date))
              .ForMember(d => d.citizenGender, opt => opt.MapFrom(src => (GenderEnum)Enum.Parse(typeof(GenderEnum), src.citizen_gender)))
              .ForMember(d => d.genderName, opt => opt.MapFrom(src => src.citizen_gender))
            .ForMember(d => d.citizenAddress, opt => opt.MapFrom(src => src.citizen_address))
            .ForMember(d => d.fkTehsil, opt => opt.MapFrom(src => src.fk_tehsil))
            .ForMember(d => d.fkDistrict, opt => opt.MapFrom(src => src.tbl_citizen_tehsil.fk_district))
            .ForMember(d => d.fkProvince, opt => opt.MapFrom(src => src.tbl_citizen_tehsil.tbl_district.fk_province))
            .ForMember(d => d.tehsilName, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tehsil_name : ""))
            .ForMember(d => d.districtName, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tbl_district.district_name : ""))
            .ForMember(d => d.provinceName, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tbl_district.tbl_province.province_name : ""))
            .ForMember(d => d.fkEmployment, opt => opt.MapFrom(src => src.fk_citizen_employment))
            .ForMember(d => d.employmentName, opt => opt.MapFrom(src => src.tbl_employment_other_specification != null ? src.tbl_employment_other_specification.employment_other_specification : (src.tbl_citizen_employment != null ? src.tbl_citizen_employment.employment_name : "")))
            .ForMember(d => d.fkEducation, opt => opt.MapFrom(src => src.fk_citizen_education))
            .ForMember(d => d.educationName, opt => opt.MapFrom(src => src.tbl_citizen_education != null ? src.tbl_citizen_education.education_name : ""))
            .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.citizen_cnic))
            .ForMember(d => d.ibanNo, opt => opt.MapFrom((src) => src.tbl_citizen_family_bank_info.iban_no))
            .ForMember(d => d.accountHolderName, opt => opt.MapFrom((src) => src.tbl_citizen_family_bank_info.account_holder_name))
            .ForMember(d => d.aIOA, opt => opt.MapFrom((src) => src.tbl_citizen_family_bank_info.family_income))
            .ForMember(d => d.fkBank, opt => opt.MapFrom(src => (src.tbl_citizen_family_bank_info.fk_bank)))
            .ForMember(d => d.bankName, opt => opt.MapFrom((src) => src.tbl_citizen_family_bank_info.tbl_bank_other_specification != null ? src.tbl_citizen_family_bank_info.tbl_bank_other_specification.bank_other_specification : src.tbl_citizen_family_bank_info.tbl_bank.bank_name))
            .ForMember(d => d.uniHHId, opt => opt.MapFrom(src => (src.unique_hh_id).ToString()))
             .ForMember(d => d.pmt, opt => opt.MapFrom(src => (src.pmt)))
             .ForMember(d => d.submissionDate, opt => opt.MapFrom(src => (src.submission_date).ToString()))
             .ForMember(d => d.isValidBeneficiary, opt => opt.MapFrom(src => (src.is_valid_beneficiary)));
            #endregion
            #region Enrolled Citizen
            CreateMap<AddEnrollmentDTO, tbl_citizen>()
             .ForMember(d => d.citizen_name, opt => opt.MapFrom(src => src.citizenName))
             .ForMember(d => d.citizen_phone_no, opt => opt.MapFrom(src => src.citizenPhoneNo))
             .ForMember(d => d.citizen_gender, opt => opt.MapFrom(src => $"{(GenderEnum)src.citizenGender}"))
             .ForMember(d => d.citizen_martial_status, opt => opt.MapFrom(src => $"{(MartialStatusEnum)src.maritalStatus}"))
             .ForMember(d => d.citizen_address, opt => opt.MapFrom(src => src.citizenAddress))
             .ForMember(d => d.citizen_father_spouce_name, opt => opt.MapFrom(src => src.fatherSpouseName))
             .ForMember(d => d.fk_tehsil, opt => opt.MapFrom(src => Guid.Parse(src.fkTehsil)))
             .ForMember(d => d.fk_citizen_employment, opt => opt.MapFrom(src => Guid.Parse(src.fkEmployment)))
             .ForMember(d => d.fk_citizen_education, opt => opt.MapFrom(src => Guid.Parse(src.fkEducation)))
             .ForMember(d => d.citizen_date_of_birth, opt => opt.MapFrom((src, dest) => otherServices.Check(src.dateOfBirth) ? DateTime.Parse(src.dateOfBirth) : dest.citizen_date_of_birth))
             .ForMember(d => d.citizen_cnic, opt => opt.MapFrom(src => src.citizenCnic))
             .ForMember(d => d.unique_hh_id, opt => opt.MapFrom((src, dest) => !string.IsNullOrEmpty(src.uniHHId) ? decimal.Parse(src.uniHHId) : dest.unique_hh_id))
             .ForMember(d => d.pmt, opt => opt.MapFrom((src, dest) => (!string.IsNullOrEmpty(src.pmt) ? src.pmt :dest.pmt )))
             .ForMember(d => d.submission_date, opt => opt.MapFrom((src, dest) => !string.IsNullOrEmpty(src.submissionDate) ? DateTime.Parse(src.submissionDate) : dest.submission_date!))
             .ForMember(d => d.is_valid_beneficiary, opt => opt.MapFrom((src, dest) => (dest.is_valid_beneficiary)));
             CreateMap<UpdateEnrollmentDTO, tbl_citizen>()
             .ForMember(d => d.citizen_id, opt => opt.MapFrom((src, dest) => dest.citizen_id))
             .ForMember(d => d.citizen_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenName) ? src.citizenName : dest.citizen_name))
             .ForMember(d => d.citizen_phone_no, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenPhoneNo) ? src.citizenPhoneNo : dest.citizen_phone_no))
             .ForMember(d => d.citizen_martial_status, opt => opt.MapFrom((src, dest) => otherServices.Check(src.maritalStatus) ? $"{(MartialStatusEnum)src.maritalStatus}" : dest.citizen_martial_status))
             .ForMember(d => d.citizen_gender, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenGender) ? $"{(GenderEnum)src.citizenGender}" : dest.citizen_gender))
             .ForMember(d => d.citizen_address, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenAddress) ? src.citizenAddress : dest.citizen_address))
             .ForMember(d => d.fk_tehsil, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkTehsil) ? Guid.Parse(src.fkTehsil) : dest.fk_tehsil))
             .ForMember(d => d.fk_citizen_education, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkEducation) ? Guid.Parse(src.fkEducation) : dest.fk_citizen_education))
             .ForMember(d => d.fk_citizen_employment, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkEmployment) ? Guid.Parse(src.fkEmployment) : dest.fk_citizen_employment))
             .ForMember(d => d.citizen_father_spouce_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fatherSpouseName) ? src.fatherSpouseName : dest.citizen_father_spouce_name))
             .ForMember(d => d.citizen_date_of_birth, opt => opt.MapFrom((src, dest) => otherServices.Check(src.dateOfBirth) ? DateTime.Parse(src.dateOfBirth) : dest.citizen_date_of_birth))
             .ForMember(d => d.citizen_cnic, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenCnic) ? src.citizenCnic : dest.citizen_cnic))
             .ForMember(d => d.unique_hh_id, opt => opt.MapFrom((src, dest) => !string.IsNullOrEmpty(src.uniHHId) ? decimal.Parse(src.uniHHId) : dest.unique_hh_id))
             .ForMember(d => d.pmt, opt => opt.MapFrom((src, dest) => (!string.IsNullOrEmpty(src.pmt) ? src.pmt : dest.pmt)))
             .ForMember(d => d.submission_date, opt => opt.MapFrom((src, dest) => !string.IsNullOrEmpty(src.submissionDate) ? DateTime.Parse(src.submissionDate) : dest.submission_date))
             .ForMember(d => d.is_valid_beneficiary, opt => opt.MapFrom((src, dest) => (dest.is_valid_beneficiary)));          
            CreateMap<tbl_enrollment, EnrollmentResponseDTO>()
             .ForMember(d => d.enrollmentId, opt => opt.MapFrom(src => src.enrollment_id))
             .ForMember(d => d.citizenId, opt => opt.MapFrom(src => src.tbl_citizen.citizen_id))
             .ForMember(d => d.citizenCode, opt => opt.MapFrom(src => src.tbl_citizen.id))
             .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_name))
             .ForMember(d => d.enrolledDate, opt => opt.MapFrom(src => src.enrolled_date))
             .ForMember(d => d.fatherSpouseName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_name))
             .ForMember(d => d.citizenPhoneNo, opt => opt.MapFrom(src => src.tbl_citizen.citizen_father_spouce_name))
             .ForMember(d => d.maritalStatus, opt => opt.MapFrom(src => (MartialStatusEnum)Enum.Parse(typeof(MartialStatusEnum), src.tbl_citizen.citizen_martial_status)))
             .ForMember(d => d.maritalStatusName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_martial_status))
             .ForMember(d => d.citizenGender, opt => opt.MapFrom(src => (GenderEnum)Enum.Parse(typeof(GenderEnum), src.tbl_citizen.citizen_gender)))
             .ForMember(d => d.genderName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_gender))
             .ForMember(d => d.citizenAddress, opt => opt.MapFrom(src => src.tbl_citizen.citizen_address))
             .ForMember(d => d.dateOfBirth, opt => opt.MapFrom(src => src.tbl_citizen.citizen_date_of_birth))
             .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.tbl_citizen.citizen_cnic))
             .ForMember(d => d.ibanNo, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_bank_info.iban_no))
             .ForMember(d => d.accountTypeName, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_bank_info.account_type))
             .ForMember(d => d.BankName, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_bank_info.tbl_bank_other_specification != null ? src.tbl_citizen.tbl_citizen_bank_info.tbl_bank_other_specification.bank_other_specification : src.tbl_citizen.tbl_citizen_bank_info.tbl_bank.bank_name))          
             .ForMember(d => d.citizenSchemeYear, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_scheme.citizen_scheme_year))
             .ForMember(d => d.citizenSchemeQuarter, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_scheme.citizen_scheme_quarter))
             .ForMember(d => d.quarterCode, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_scheme.citizen_scheme_quarter_code))
             .ForMember(d => d.citizenSchemeStartingMonth, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_scheme.citizen_scheme_starting_month))
             .ForMember(d => d.fkTehsil, opt => opt.MapFrom(src => src.tbl_citizen.fk_tehsil))
             .ForMember(d => d.fkDistrict, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_tehsil.fk_district))
             .ForMember(d => d.fkProvince, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_tehsil.tbl_district.fk_province))
             .ForMember(d => d.tehsilName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_tehsil != null ? src.tbl_citizen.tbl_citizen_tehsil.tehsil_name : ""))
             .ForMember(d => d.districtName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_tehsil != null ? src.tbl_citizen.tbl_citizen_tehsil.tbl_district.district_name : ""))
             .ForMember(d => d.provinceName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_tehsil != null ? src.tbl_citizen.tbl_citizen_tehsil.tbl_district.tbl_province.province_name : ""))
             .ForMember(d => d.fkEmployment, opt => opt.MapFrom(src => src.tbl_citizen.fk_citizen_employment))
             .ForMember(d => d.fkEnrolledBy, opt => opt.MapFrom(src => src.fk_enrolled_by))
             .ForMember(d => d.enrolledByUser, opt => opt.MapFrom(src => src.enrolled_by.user_name))
             .ForMember(d => d.employmentName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_employment != null ? src.tbl_citizen.tbl_citizen_employment.employment_name : ""))
             .ForMember(d => d.fkEducation, opt => opt.MapFrom(src => src.tbl_citizen.fk_citizen_education))
             .ForMember(d => d.educationName, opt => opt.MapFrom(src => src.tbl_citizen.tbl_citizen_education != null ? src.tbl_citizen.tbl_citizen_education.education_name : ""))
             .ForMember(d => d.citizenSchemeSavingAmount, opt => opt.MapFrom((src) => src.tbl_citizen.tbl_citizen_scheme.citizen_scheme_saving_amount));
            CreateMap<tbl_citizen, EnrollmentResponseDTO>()
             .ForMember(d => d.enrollmentId, opt => opt.MapFrom(src => src.tbl_enrollment != null ? src.tbl_enrollment.enrollment_id.ToString() : ""))
             .ForMember(d => d.citizenId, opt => opt.MapFrom(src => src.citizen_id))
             .ForMember(d => d.citizenCode, opt => opt.MapFrom(src => src.id))
             .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.citizen_name))
             .ForMember(d => d.fatherSpouseName, opt => opt.MapFrom(src => src.citizen_name))
             .ForMember(d => d.citizenPhoneNo, opt => opt.MapFrom(src => src.citizen_phone_no))
             .ForMember(d => d.maritalStatus, opt => opt.MapFrom(src => (MartialStatusEnum)Enum.Parse(typeof(MartialStatusEnum), src.citizen_martial_status)))
             .ForMember(d => d.maritalStatusName, opt => opt.MapFrom(src => src.citizen_martial_status))
             .ForMember(d => d.citizenGender, opt => opt.MapFrom(src => (GenderEnum)Enum.Parse(typeof(GenderEnum), src.citizen_gender)))
             .ForMember(d => d.genderName, opt => opt.MapFrom(src => src.citizen_gender))
             .ForMember(d => d.citizenAddress, opt => opt.MapFrom(src => src.citizen_address))
             .ForMember(d => d.dateOfBirth, opt => opt.MapFrom(src => src.citizen_date_of_birth))
             .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.citizen_cnic))
             .ForMember(d => d.ibanNo, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.iban_no))
             .ForMember(d => d.accountTypeName, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.account_type))
             .ForMember(d => d.BankName, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.tbl_bank_other_specification != null ? src.tbl_citizen_bank_info.tbl_bank_other_specification.bank_other_specification : src.tbl_citizen_bank_info.tbl_bank.bank_name))
             .ForMember(d => d.citizenSchemeYear, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_year))
             .ForMember(d => d.citizenSchemeQuarter, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_quarter))
             .ForMember(d => d.quarterCode, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_quarter_code))
             .ForMember(d => d.citizenSchemeStartingMonth, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_starting_month))
             .ForMember(d => d.fkTehsil, opt => opt.MapFrom(src => src.fk_tehsil))
             .ForMember(d => d.fkDistrict, opt => opt.MapFrom(src => src.tbl_citizen_tehsil.fk_district))
             .ForMember(d => d.fkProvince, opt => opt.MapFrom(src => src.tbl_citizen_tehsil.tbl_district.fk_province))
             .ForMember(d => d.tehsilName, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tehsil_name : ""))
             .ForMember(d => d.districtName, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tbl_district.district_name : ""))
             .ForMember(d => d.provinceName, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tbl_district.tbl_province.province_name : ""))
             .ForMember(d => d.fkEmployment, opt => opt.MapFrom(src => src.fk_citizen_employment))
             .ForMember(d => d.employmentName, opt => opt.MapFrom(src => src.tbl_employment_other_specification != null ? src.tbl_employment_other_specification.employment_other_specification : (src.tbl_citizen_employment != null ? src.tbl_citizen_employment.employment_name : "")))
             .ForMember(d => d.fkEducation, opt => opt.MapFrom(src => src.fk_citizen_education))
             .ForMember(d => d.educationName, opt => opt.MapFrom(src => src.tbl_citizen_education != null ? src.tbl_citizen_education.education_name : ""))
             .ForMember(d => d.citizenSchemeSavingAmount, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_saving_amount))
             .ForMember(d => d.uniHHId, opt => opt.MapFrom(src => (src.unique_hh_id).ToString()))
             .ForMember(d => d.pmt, opt => opt.MapFrom(src => (src.pmt)))
             .ForMember(d => d.submissionDate, opt => opt.MapFrom(src => (src.submission_date).ToString()))
             .ForMember(d => d.isValidBeneficiary, opt => opt.MapFrom(src => (src.is_valid_beneficiary)));
            #endregion
            #region Citizen
            CreateMap<tbl_citizen, CitizenResponseDTO>()
              .ForMember(d => d.enrollmentId, opt => opt.MapFrom(src => src.tbl_enrollment != null ? src.tbl_enrollment.enrollment_id.ToString() : ""))
              .ForMember(d => d.registrationId, opt => opt.MapFrom(src => src.tbl_citizen_registration != null ? src.tbl_citizen_registration.registration_id.ToString() : ""))
              .ForMember(d => d.citizenId, opt => opt.MapFrom(src => src.citizen_id))
              .ForMember(d => d.citizenCode, opt => opt.MapFrom(src => src.id))
              .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.citizen_name))
              .ForMember(d => d.citizenPhoneNo, opt => opt.MapFrom(src => src.citizen_phone_no))
              .ForMember(d => d.registrationDate, opt => opt.MapFrom(src => src.tbl_citizen_registration.registered_date))
              .ForMember(d => d.citizenGender, opt => opt.MapFrom(src => (GenderEnum)Enum.Parse(typeof(GenderEnum), src.citizen_gender)))
              .ForMember(d => d.genderName, opt => opt.MapFrom(src => src.citizen_gender))
              .ForMember(d => d.citizenAddress, opt => opt.MapFrom(src => src.citizen_address))
              .ForMember(d => d.fkTehsil, opt => opt.MapFrom(src => src.fk_tehsil))
              .ForMember(d => d.fkDistrict, opt => opt.MapFrom(src => src.tbl_citizen_tehsil.fk_district))
              .ForMember(d => d.fkProvince, opt => opt.MapFrom(src => src.tbl_citizen_tehsil.tbl_district.fk_province))
              .ForMember(d => d.tehsilName, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tehsil_name : ""))
              .ForMember(d => d.districtName, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tbl_district.district_name : ""))
              .ForMember(d => d.provinceName, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tbl_district.tbl_province.province_name : ""))
              .ForMember(d => d.fkEmployment, opt => opt.MapFrom(src => src.fk_citizen_employment))
              .ForMember(d => d.employmentName, opt => opt.MapFrom(src => src.tbl_employment_other_specification != null ? src.tbl_employment_other_specification.employment_other_specification : (src.tbl_citizen_employment != null ? src.tbl_citizen_employment.employment_name : "")))
              .ForMember(d => d.fkEducation, opt => opt.MapFrom(src => src.fk_citizen_education))
              .ForMember(d => d.educationName, opt => opt.MapFrom(src => src.tbl_citizen_education != null ? src.tbl_citizen_education.education_name : ""))
              .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.citizen_cnic))
              .ForMember(d => d.ibanNo, opt => opt.MapFrom((src) => src.tbl_citizen_family_bank_info.iban_no))
              .ForMember(d => d.accountHolderName, opt => opt.MapFrom((src) => src.tbl_citizen_family_bank_info.account_holder_name))
              .ForMember(d => d.aIOA, opt => opt.MapFrom((src) => src.tbl_citizen_family_bank_info.family_income))
              .ForMember(d => d.fkBank, opt => opt.MapFrom(src => (src.tbl_citizen_family_bank_info.fk_bank)))
              .ForMember(d => d.bankName, opt => opt.MapFrom((src) => src.tbl_citizen_family_bank_info.tbl_bank_other_specification != null ? src.tbl_citizen_family_bank_info.tbl_bank_other_specification.bank_other_specification : src.tbl_citizen_family_bank_info.tbl_bank.bank_name))
              .ForMember(d => d.uniHHId, opt => opt.MapFrom(src => (src.unique_hh_id).ToString()))
              .ForMember(d => d.pmt, opt => opt.MapFrom(src => (src.pmt)))
              .ForMember(d => d.submissionDate, opt => opt.MapFrom(src => (src.submission_date).ToString()))
              .ForMember(d => d.isValidBeneficiary, opt => opt.MapFrom(src => (src.is_valid_beneficiary)))
              .ForMember(d => d.fatherSpouseName, opt => opt.MapFrom(src => src.citizen_name))
              .ForMember(d => d.maritalStatus, opt => opt.MapFrom(src => (MartialStatusEnum)Enum.Parse(typeof(MartialStatusEnum), src.citizen_martial_status)))
              .ForMember(d => d.maritalStatusName, opt => opt.MapFrom(src => src.citizen_martial_status))
              .ForMember(d => d.dateOfBirth, opt => opt.MapFrom(src => src.citizen_date_of_birth))
              .ForMember(d => d.citizenIbanNo, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.iban_no))
              .ForMember(d => d.citizenAccountTypeName, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.account_type))
              .ForMember(d => d.citizenBankName, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.tbl_bank_other_specification != null ? src.tbl_citizen_bank_info.tbl_bank_other_specification.bank_other_specification : src.tbl_citizen_bank_info.tbl_bank.bank_name))
              .ForMember(d => d.citizenSchemeYear, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_year))
              .ForMember(d => d.citizenSchemeQuarter, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_quarter))
              .ForMember(d => d.quarterCode, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_quarter_code))
              .ForMember(d => d.citizenSchemeStartingMonth, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_starting_month))
              .ForMember(d => d.citizenSchemeSavingAmount, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_saving_amount));
            #endregion
        }
    }
}
