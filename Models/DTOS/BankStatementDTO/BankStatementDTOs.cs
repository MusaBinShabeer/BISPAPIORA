using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.BankStatementDTO
{
    public class BankStatementDTO
    {
        public string bankStatementName { get; set; } = string.Empty;
        public byte[] bankStatementData { get; set; }
        public string bankStatementContentType { get; set; } = string.Empty;
        public string bankStatementCnic { get; set; } = string.Empty;
        public string fkCitizenCompliance { get; set; } = string.Empty;
    }
    public class AddBankStatementDTO : BankStatementDTO
    {
        [Required]
        public new string bankStatementName { get; set; } = string.Empty;
        [Required]
        public new byte[] bankStatementData { get; set; }
    }
    public class UpdateBankStatementDTO : BankStatementDTO
    {
        [Required]
        public string bankStatementId { get; set; } = string.Empty;
    }
    public class BankStatementResponseDTO : BankStatementDTO
    {
        public string bankStatementId { get; set; } = string.Empty;
        public string citizenName { get; set; } = string.Empty;
        public string citizenCnic { get; set; } = string.Empty;
        public int bankStatementCode { get; set; } = default!;
    }
}
