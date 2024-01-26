using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.BankDTO
{
    public class BankDTO
    {
        public string bankName { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
    }
    public class AddBankDTO : BankDTO
    {
        [Required]
        public new string bankName { get; set; } = string.Empty;
    }
    public class UpdateBankDTO : BankDTO
    {
        [Required]
        public string bankId { get; set; } = string.Empty;
    }
    public class BankResponseDTO : BankDTO
    {
        public string bankId { get; set; } = string.Empty;
    }
}
