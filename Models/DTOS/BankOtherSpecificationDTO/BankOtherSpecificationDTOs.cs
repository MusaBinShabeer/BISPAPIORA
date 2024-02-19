using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO
{
    public class AddBankOtherSpecificationDTO
    {
        public string bankOtherSpecification { get; set; } = string.Empty;
        public string fkCitizen { get; set; } = string.Empty;
    }
    public class UpdateBankOtherSpecificationDTO : AddBankOtherSpecificationDTO
    {
        public string bankOtherSpecificationId { get; set; } = string.Empty;
    }
    public class BankOtherSpecificationResponseDTO : AddBankOtherSpecificationDTO
    {
        public string bankOtherSpecificationId { get; set; } = string.Empty;
        public int bankOtherSpecificationCode { get; set; } = default!;
    }
}
