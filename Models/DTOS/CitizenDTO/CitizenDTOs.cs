using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.CitizenDTO
{
    public class CitizenDTOs
    {
        public string citizenCnic { get; set; } = string.Empty;
        public string citizenName { get; set; } = string.Empty;
        public string citizenPhoneNo { get; set; } = string.Empty;
        public int citizenGender { get; set; } = 0;
        public string citizenAddress { get; set; } = string.Empty;
        public string citizenBankOtherSpecification { get; set; } = string.Empty;
        public string citizenEmploymentOtherSpecification { get; set; } = string.Empty;
        public bool isValidBeneficiary { get; set; } = true;
        public string pmt { get; set; } = string.Empty;
        public string uniHHId { get; set; } = string.Empty;
        public string submissionDate { get; set; } = string.Empty;
    }
    public class CitizenResponseDTO:CitizenDTOs
    {
        public string enrollmentId { get; set; } = string.Empty;
        public string registrationId { get; set; } = string.Empty;
        public string citizenId { get; set; } = string.Empty;
        public string tehsilName { get; set; } = string.Empty;
        public string districtName { get; set; } = string.Empty;
        public string provinceName { get; set; } = string.Empty;
        public string employmentName { get; set; } = string.Empty;
        public string fkEmployment { get; set; } = string.Empty;
        public string educationName { get; set; } = string.Empty;
        public string fkEducation { get; set; } = string.Empty;
        public string genderName { get; set; } = string.Empty;
        public string bankName { get; set; } = string.Empty;
        public bool isRegisteered { get; set; } = false;
        public bool isEnrolled { get; set; } = false;
        public string registrationDate { get; set; } = string.Empty;
        public string fkTehsil { get; set; } = string.Empty;
        public string fkProvince { get; set; } = string.Empty;
        public string fkDistrict { get; set; } = string.Empty;
        public int citizenCode { get; set; } = default!;
        public string registeredByUser { get; set; } = string.Empty;
        public string maritalStatusName { get; set; } = string.Empty;
        public string accountTypeName { get; set; } = string.Empty;      
        public string BankName { get; set; } = string.Empty;
        public string fkBank { get; set; } = string.Empty;
        public string enrolledDate { get; set; } = string.Empty;
        public string enrolledByUser { get; set; } = string.Empty;
        public string ibanNo { get; set; } = string.Empty;
        public string accountHolderName { get; set; } = string.Empty;
        public string aIOA { get; set; } = string.Empty;
        public string citizenIbanNo { get; set; } = string.Empty;
        public string citizenAccountTypeName { get; set; } = string.Empty;
        public string fatherSpouseName { get; set; } = string.Empty;
        public string maritalStatus { get; set; } = string.Empty;
        public string dateOfBirth { get; set; } = string.Empty;
        public string citizenSchemeYear { get; set; } = string.Empty;
        public string citizenSchemeQuarter { get; set; } = string.Empty;
        public string quarterCode { get; set; } = string.Empty;
        public string citizenSchemeStartingMonth { get; set; } = string.Empty;
        public string citizenSchemeSavingAmount { get; set; } = string.Empty;
        public string citizenBankName { get; set; } = string.Empty;
        public string imageCitizenAttachmentName { get; set; } = string.Empty;
        public string imageCitizenAttachmentData { get; set; } = string.Empty;
        public string imageCitizenAttachmentContentType { get; set; } = string.Empty;
        public string imageCitizenFingerPrintName { get; set; } = string.Empty;
        public string imageCitizenFingerPrintData { get; set; } = string.Empty;
        public string imageCitizenFingerPrintContentType { get; set; } = string.Empty;
        public string imageCitizenThumbPrintName { get; set; } = string.Empty;
        public string imageCitizenThumbPrintData { get; set; } = string.Empty;
        public string imageCitizenThumbPrintContentType { get; set; } = string.Empty;
    }
}
