﻿using System.ComponentModel.DataAnnotations;
using BISPAPIORA.Models.DTOS.CitizenDTO;

namespace BISPAPIORA.Models.DTOS.EnrollmentDTO
{
    public class EnrollmentDTO : CitizenDTOs
    {
        public int maritalStatus { get; set; } = 0;
        public string fatherSpouseName { get; set; } = string.Empty;
        public string dateOfBirth { get; set; } = string.Empty;
        public string fkCitizen { get; set; } = string.Empty;
        public string fkBank { get; set; } = string.Empty;
        public string ibanNo { get; set; } = string.Empty;
        public string fkTehsil { get; set; } = string.Empty;
        public string fkEmployment { get; set; } = string.Empty;
        public string fkEducation { get; set; } = string.Empty;
        public int accountType { get; set; } = 0;
        public string citizenSchemeYear { get; set; } = string.Empty;
        public  string citizenSchemeQuarter { get; set; } = string.Empty;
        public  string citizenSchemeStartingMonth { get; set; } = string.Empty;
        public  double citizenSchemeSavingAmount { get; set; } = 0.0;
        public string fileName { get; set; } = string.Empty;
        public string filePath { get; set; } = string.Empty;
        public string thumbPrintName { get; set; } = string.Empty;
        public string thumbPrintPath { get; set; } = string.Empty;
        public int quarterCode { get; set; } = 0;
    }
    public class AddEnrollmentDTO : EnrollmentDTO
    {
        [Required]
        public new int maritalStatus { get; set; } = 0;
        [Required]
        public new string fkBank { get; set; } = string.Empty;
        [Required]
        public new string ibanNo { get; set; } = string.Empty;
        [Required]
        public new int accountType { get; set; } = 0;
        [Required]
        public new string citizenSchemeYear { get; set; } = string.Empty;
        [Required]
        public new string citizenSchemeQuarter { get; set; } = string.Empty;
        [Required]
        public new string citizenSchemeStartingMonth { get; set; } = string.Empty;
        [Required]
        public new double citizenSchemeSavingAmount { get; set; } = 0.0;
        [Required]
        public new int quarterCode { get; set; } = 0;
        [Required]
        public new string fkTehsil { get; set; } = string.Empty;
        [Required]
        public new string fkEmployment { get; set; } = string.Empty;
        [Required]
        public new string fkEducation { get; set; } = string.Empty;
    }
    public class UpdateEnrollmentDTO : EnrollmentDTO
    {
        public string enrollmentId { get; set; } = string.Empty;
    }
    public class EnrollmentResponseDTO : EnrollmentDTO
    {
        public string citizenId { get; set; } = string.Empty;
        public string enrollmentId { get; set; } = string.Empty;
        public string maritalStatusName { get; set; } = string.Empty;
        public string accountTypeName { get; set; } = string.Empty;
        public string tehsilName { get; set; } = string.Empty;
        public string districtName { get; set; } = string.Empty;
        public string provinceName { get; set; } = string.Empty;
        public string educationName { get; set; } = string.Empty;
        public string employmentName { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string genderName { get; set; } = string.Empty;
        public string fkProvince { get; set; } = string.Empty;
        public string fkDistrict { get; set; } = string.Empty;
        public int citizenCode { get; set; } = default!;
    }
}
