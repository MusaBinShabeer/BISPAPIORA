using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO
{
    public class AddRegisteredBankOtherSpecificationDTO
    {
        public string bankOtherSpecification { get; set; } = string.Empty;
        public string fkCitizenFamilyBankInfo { get; set; } = string.Empty;
    }
    public class AddEnrolledBankOtherSpecificationDTO
    {
        public string bankOtherSpecification { get; set; } = string.Empty;
        public string fkCitizenBankInfo { get; set; } = string.Empty;
    }
    public class UpdateRegisteredBankOtherSpecificationDTO : AddRegisteredBankOtherSpecificationDTO
    {
        public string bankOtherSpecificationId { get; set; } = string.Empty;
    }
    public class UpdateEnrolledBankOtherSpecificationDTO : AddEnrolledBankOtherSpecificationDTO
    {
        public string bankOtherSpecificationId { get; set; } = string.Empty;
    }
    public class BankRegisteredOtherSpecificationResponseDTO : AddRegisteredBankOtherSpecificationDTO
    {
        public string bankOtherSpecificationId { get; set; } = string.Empty;
    }
}
