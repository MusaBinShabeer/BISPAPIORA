﻿using AutoMapper;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankDTO;
using BISPAPIORA.Models.DTOS.CitizenDTO;
using BISPAPIORA.Models.DTOS.DashboardDTO;
using BISPAPIORA.Models.ENUMS;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class DashboardProfiles : Profile
    {
        private readonly OtherServices otherServices = new();
        public DashboardProfiles()
        {
            CreateMap<tbl_citizen, DashboardCitizenBaseModel>()
                .ForMember(d => d.registered_date, opt => opt.MapFrom(src => src.tbl_citizen_registration != null ? src.tbl_citizen_registration.registered_date : default!))
                .ForMember(d => d.enrolled_date, opt => opt.MapFrom(src => src.tbl_enrollment != null ? src.tbl_enrollment.enrolled_date : default!))
                .ForMember(d => d.enrolled_by, opt => opt.MapFrom(src => src.tbl_enrollment != null ? src.tbl_enrollment.enrolled_by.user_id : default!))
                .ForMember(d => d.registered_by, opt => opt.MapFrom(src => src.tbl_citizen_registration != null ? src.tbl_citizen_registration.registered_by.user_id : default!))
                .ForMember(d => d.citizen_name, opt => opt.MapFrom(src => src.citizen_name))
                .ForMember(d => d.user_name, opt => opt.MapFrom(src => src.tbl_citizen_registration != null ? src.tbl_citizen_registration.registered_by.user_name : src.tbl_enrollment != null ? src.tbl_enrollment.enrolled_by.user_name : ""))
                .ForMember(d => d.citizen_id, opt => opt.MapFrom(src => src.citizen_id))
                .ForMember(d => d.citizen_scheme_id, opt => opt.MapFrom(src => src.tbl_citizen_scheme.citizen_scheme_id))
                .ForMember(d => d.saving_amount, opt => opt.MapFrom(src => src.tbl_citizen_scheme.citizen_scheme_saving_amount))
                .ForMember(d => d.registration, opt => opt.MapFrom(src => src.tbl_citizen_registration))
                .ForMember(d => d.enrollment, opt => opt.MapFrom(src => src.tbl_enrollment)); 
            CreateMap<tbl_citizen, CitizenBaseModel>()
              .ForMember(d => d.tbl_citizen_registration, opt => opt.MapFrom(src => src.tbl_citizen_registration ))
              .ForMember(d => d.tbl_enrollment, opt => opt.MapFrom(src => src.tbl_enrollment))
              .ForMember(d => d.tbl_citizen_family_bank_info, opt => opt.MapFrom(src => src.tbl_citizen_family_bank_info))
              .ForMember(d => d.tbl_citizen_bank_info, opt => opt.MapFrom(src => src.tbl_citizen_bank_info))
              .ForMember(d => d.citizen_name, opt => opt.MapFrom(src => src.citizen_name))
              .ForMember(d => d.tbl_citizen_compliances, opt => opt.MapFrom(src => src.tbl_citizen_compliances))
              .ForMember(d => d.tbl_citizen_education, opt => opt.MapFrom(src => src.tbl_citizen_education))
              .ForMember(d => d.tbl_citizen_employment, opt => opt.MapFrom(src => src.tbl_citizen_employment))
              .ForMember(d => d.tbl_citizen_scheme, opt => opt.MapFrom(src => src.tbl_citizen_scheme))
              .ForMember(d => d.tbl_citizen_tehsil, opt => opt.MapFrom(src => src.tbl_citizen_tehsil))
              .ForMember(d => d.tbl_employment_other_specification, opt => opt.MapFrom(src => src.tbl_employment_other_specification))
              .ForMember(d => d.tbl_image_citizen_attachment, opt => opt.MapFrom(src => src.tbl_image_citizen_attachment))
              .ForMember(d => d.tbl_image_citizen_finger_print, opt => opt.MapFrom(src => src.tbl_image_citizen_finger_print))
              .ForMember(d => d.tbl_transactions, opt => opt.MapFrom(src => src.tbl_transactions))
              .ForMember(d => d.citizen_date_of_birth, opt => opt.MapFrom(src => src.citizen_date_of_birth))
              .ForMember(d => d.citizen_father_spouce_name, opt => opt.MapFrom(src => src.citizen_father_spouce_name))
              .ForMember(d => d.citizen_address, opt => opt.MapFrom(src => src.citizen_address))
              .ForMember(d => d.citizen_cnic, opt => opt.MapFrom(src => src.citizen_cnic))
              .ForMember(d => d.citizen_gender, opt => opt.MapFrom(src => src.citizen_gender))
              .ForMember(d => d.citizen_martial_status, opt => opt.MapFrom(src => src.citizen_martial_status))
              .ForMember(d => d.citizen_phone_no, opt => opt.MapFrom(src => src.citizen_phone_no))
              .ForMember(d => d.fk_citizen_education, opt => opt.MapFrom(src => src.fk_citizen_education))
              .ForMember(d => d.fk_citizen_employment, opt => opt.MapFrom(src => src.fk_citizen_employment))
              .ForMember(d => d.fk_tehsil, opt => opt.MapFrom(src => src.fk_tehsil))
              .ForMember(d => d.is_valid_beneficiary, opt => opt.MapFrom(src => src.is_valid_beneficiary))
              .ForMember(d => d.pmt, opt => opt.MapFrom(src => src.pmt))
              .ForMember(d => d.submission_date, opt => opt.MapFrom(src => src.submission_date))
              .ForMember(d => d.id, opt => opt.MapFrom(src => src.id))
              .ForMember(d => d.unique_hh_id, opt => opt.MapFrom(src => src.unique_hh_id))
              .ForMember(d => d.citizen_id, opt => opt.MapFrom(src => src.citizen_id));
            CreateMap<tbl_citizen, DashboardCitizenLocationModel>()
                .ForMember(d => d.citizen_id, opt => opt.MapFrom(src => src.citizen_id))
                .ForMember(d => d.citizen_name, opt => opt.MapFrom(src => src.citizen_name))
                .ForMember(d => d.user_name, opt => opt.MapFrom(src => src.tbl_citizen_registration != null ? src.tbl_citizen_registration.registered_by.user_name : src.tbl_enrollment != null ? src.tbl_enrollment.enrolled_by.user_name : ""))
                .ForMember(d => d.citizen_id, opt => opt.MapFrom(src => src.citizen_id))
                .ForMember(d => d.province_name, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tbl_district.tbl_province.province_name : ""))
                .ForMember(d => d.province_id, opt => opt.MapFrom(src => src.tbl_citizen_tehsil.tbl_district.fk_province))
                .ForMember(d => d.district_name, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tbl_district.district_name : ""))
                .ForMember(d => d.district_id, opt => opt.MapFrom(src => src.tbl_citizen_tehsil.fk_district))
                .ForMember(d => d.tehsil_name, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tehsil_name : ""))
                .ForMember(d => d.tehsil_id, opt => opt.MapFrom(src => src.fk_tehsil))
                .ForMember(d => d.citizen_gender, opt => opt.MapFrom(src => src.citizen_gender))
                .ForMember(d => d.citizen_martial_status, opt => opt.MapFrom(src => src.citizen_martial_status))
                .ForMember(d => d.educationId, opt => opt.MapFrom(src => src.fk_citizen_education))
                .ForMember(d => d.educationName, opt => opt.MapFrom(src => src.tbl_citizen_education.education_name))
                .ForMember(d => d.employmentId, opt => opt.MapFrom(src => src.fk_citizen_employment))
                .ForMember(d => d.employmentName, opt => opt.MapFrom(src => src.tbl_citizen_employment.employment_name))
                .ForMember(d => d.citizen_scheme_id, opt => opt.MapFrom(src => src.tbl_citizen_scheme.citizen_scheme_id))
                .ForMember(d => d.saving_amount, opt => opt.MapFrom(src => src.tbl_citizen_scheme.citizen_scheme_saving_amount))
                .ForMember(d => d.insertion_date, opt => opt.MapFrom(src => src.insertion_date))
                .ForMember(d => d.registration, opt => opt.MapFrom(src => src.tbl_citizen_registration))
                .ForMember(d => d.enrollment, opt => opt.MapFrom(src => src.tbl_enrollment));
            CreateMap<CitizenBaseModel, CitizenResponseDTO>()
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
                   .ForMember(d => d.fkCitizenBank, opt => opt.MapFrom(src => (src.tbl_citizen_bank_info.fk_bank)))
                   .ForMember(d => d.bankName, opt => opt.MapFrom((src) => src.tbl_citizen_family_bank_info.tbl_bank_other_specification != null ? src.tbl_citizen_family_bank_info.tbl_bank_other_specification.bank_other_specification : src.tbl_citizen_family_bank_info.tbl_bank.bank_name))
                   .ForMember(d => d.citizenBankName, opt => opt.MapFrom((src) => src.tbl_citizen_bank_info.tbl_bank_other_specification != null ? src.tbl_citizen_bank_info.tbl_bank_other_specification.bank_other_specification : src.tbl_citizen_bank_info.tbl_bank.bank_name))
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
                   .ForMember(d => d.citizenSchemeSavingAmount, opt => opt.MapFrom((src) => src.tbl_citizen_scheme.citizen_scheme_saving_amount))
                   .ForMember(d => d.imageCitizenAttachmentName, opt => opt.MapFrom((src) => src.tbl_image_citizen_attachment.name))
                   .ForMember(d => d.imageCitizenAttachmentData, opt => opt.MapFrom((src) => src.tbl_image_citizen_attachment.data))
                   .ForMember(d => d.imageCitizenAttachmentContentType, opt => opt.MapFrom((src) => src.tbl_image_citizen_attachment.content_type))
                   .ForMember(d => d.imageCitizenFingerPrintName, opt => opt.MapFrom((src) => src.tbl_image_citizen_finger_print.finger_print_name))
                   .ForMember(d => d.imageCitizenFingerPrintData, opt => opt.MapFrom((src) => src.tbl_image_citizen_finger_print.finger_print_data))
                   .ForMember(d => d.imageCitizenFingerPrintContentType, opt => opt.MapFrom((src) => src.tbl_image_citizen_finger_print.finger_print_content_type))
                   .ForMember(d => d.imageCitizenThumbPrintName, opt => opt.MapFrom((src) => src.tbl_image_citizen_finger_print.thumb_print_content_type))
                   .ForMember(d => d.imageCitizenThumbPrintData, opt => opt.MapFrom((src) => src.tbl_image_citizen_finger_print.thumb_print_data))
                   .ForMember(d => d.imageCitizenThumbPrintContentType, opt => opt.MapFrom((src) => src.tbl_image_citizen_finger_print.thumb_print_content_type));
            CreateMap<(List<DashboardCitizenBaseModel>, List<DashboardCitizenBaseModel>), DashboardUserPerformanceResponseDTO>()
                .ForMember(dest => dest.registeredCount, opt => opt.MapFrom(src => src.Item1.Count))
                .ForMember(dest => dest.enrolledCount, opt => opt.MapFrom(src => src.Item2.Count));
            CreateMap<(double registeredPerc, double enrolledPerc), DashboardCitizenCountPercentageDTO>()
                .ForMember(dest => dest.registeredPercentage, opt => opt.MapFrom(src => src.registeredPerc))
                .ForMember(dest => dest.enrolledPercentage, opt => opt.MapFrom(src => src.enrolledPerc));       
            CreateMap<(string name, int statCount), WebDashboardStats>()
                .ForMember(dest => dest.StatName, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.StatCount, opt => opt.MapFrom(src => src.statCount)); 
            CreateMap<(string name, decimal? statCount), WebDashboardStats>()
                .ForMember(dest => dest.StatName, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.StatCount, opt => opt.MapFrom(src => src.statCount));
            CreateMap<DashboardDistrictCitizenCountPercentageDTO, DashboardTehsilCitizenCountPercentageDTO>()
                .ForMember(dest => dest.tehsilName, opt => opt.Ignore())
                .ForMember(dest => dest.districtName, opt => opt.MapFrom(src => src.districtName))
                .ForMember(dest => dest.provinceName, opt => opt.MapFrom(src => src.provinceName))
                .ForMember(dest => dest.citizenPercentage, opt => opt.MapFrom(src => src.citizenPercentage));
            CreateMap<DashboardProvinceCitizenCountPercentageDTO, DashboardTehsilCitizenCountPercentageDTO>()
                .ForMember(dest => dest.tehsilName, opt => opt.Ignore())
                .ForMember(dest => dest.districtName, opt => opt.Ignore())
                .ForMember(dest => dest.provinceName, opt => opt.MapFrom(src => src.provinceName))
                .ForMember(dest => dest.citizenPercentage, opt => opt.MapFrom(src => src.citizenPercentage));

        }
    }
}
