using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.TransactionDTO
{
    public class TransactionDTO
    {
        public string transactionDate { get; set; } = string.Empty;
        public string transactionType { get; set; } = string.Empty;
        public double transactionAmount { get; set; } = 0.0;
        public int quarterCode { get; set; } = 0;
        public string fkCitizen { get; set; } = string.Empty;
        public string fkCompliance { get; set; } = string.Empty;
    }
    public class AddTransactionDTO : TransactionDTO
    {
        [Required]
        public new string transactionDate { get; set; } = string.Empty;
        [Required]
        public new string transactionType { get; set; } = string.Empty;
        [Required]
        public new double transactionAmount { get; set; } = 0.0;
        [Required]
        public new int quarterCode { get; set; } = 0;
        [Required]
        public new string fkCitizen { get; set; } = string.Empty;
        
    }
    public class UpdateTransactionDTO : TransactionDTO
    {
        [Required]
        public string transactionId { get; set; } = string.Empty;
    }
    public class TransactionResponseDTO : TransactionDTO
    {
        public string transactionId { get; set; } = string.Empty;
    }
}
