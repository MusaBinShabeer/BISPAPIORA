using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.PaymentDTO
{
    public class PaymentDTO
    {
        public double paidAmount { get; set; } = 0;
        public double dueAmount { get; set; } = 0;
        public int quarterCode { get; set; } = 0;
        public string fkCitizen { get; set; } = string.Empty;
        public string fkCitizenCompliance { get; set; } = string.Empty;
    }
    public class AddPaymentDTO : PaymentDTO
    {
        [Required]
        public new double paidAmount { get; set; } = 0;
        [Required]
        public new double dueAmount { get; set; } = 0;
        [Required]
        public new int quarterCode { get; set; } = 0;
        [Required]
        public new string fkCitizen { get; set; } = string.Empty;
        [Required]
        public new string fkCitizenCompliance { get; set; } = string.Empty;
    }
    public class UpdatePaymentDTO : PaymentDTO
    {
        [Required]
        public string paymentId { get; set; } = string.Empty;
    }
    public class PaymentResponseDTO : PaymentDTO
    {
        public string paymentId { get; set; } = string.Empty;
    }
}
