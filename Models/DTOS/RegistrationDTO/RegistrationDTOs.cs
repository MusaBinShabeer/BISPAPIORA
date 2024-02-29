using System.ComponentModel.DataAnnotations;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;
using BISPAPIORA.Models.DTOS.CitizenDTO;

namespace BISPAPIORA.Models.DTOS.RegistrationDTO
{
    public class RegistrationDTOs: CitizenDTOs
    {
        public string fkCitizen { get; set; } = string.Empty;
        public string fkTehsil { get; set; } = string.Empty;
        public string fkEmployment { get; set; } = string.Empty;
        public string fkEducation { get; set; } = string.Empty;
        public string accountHolderName { get; set; } = string.Empty;
        public double aIOA { get; set; } = 0.0;                         //Average Income Of Account
        public bool familySavingAccount { get; set; } = false;      
        public string fkBank { get; set; } = string.Empty;
        public string ibanNo { get; set; } = string.Empty;
        public string fkRegisteredBy { get; set; } = string.Empty;
    }
    public class AddRegistrationDTO : RegistrationDTOs
    {
        
        [Required]
        public new string fkTehsil { get; set; } = string.Empty;
        [Required]
        public new string fkEmployment { get; set; } = string.Empty;
        [Required]
        public new string fkEducation { get; set; } = string.Empty;
        [Required]
        public new double aIOA { get; set; } = 0.0;                         //Average Income Of Account
        [Required]
        public new bool familySavingAccount { get; set; } = false;

    }
    public class UpdateRegistrationDTO : RegistrationDTOs
    {
        [Required]
        public string registrationId { get; set; } = string.Empty;
    }
    public class RegistrationResponseDTO : RegistrationDTOs
    {
        public string enrollmentId { get; set; } = string.Empty;
        public string registrationId { get; set; } = string.Empty;
        public string citizenId { get; set; } = string.Empty;
        public string tehsilName { get; set; } = string.Empty;
        public string districtName { get; set; } = string.Empty;
        public string provinceName { get; set; } = string.Empty;
        public string employmentName { get; set; } = string.Empty;
        public string educationName { get; set; } = string.Empty;
        public string genderName { get; set; } = string.Empty;
        public string bankName { get; set; } = string.Empty;
        public bool isRegisteered { get; set; } = false;
        public bool isEnrolled { get; set; } = false;
        public string registrationDate { get; set; } = string.Empty;
        public string fkProvince { get; set; } = string.Empty;
        public string fkDistrict { get; set; } = string.Empty;
        public int citizenCode { get; set; } = default!;
        public string registeredByUser { get; set; } = string.Empty;
    }
}
