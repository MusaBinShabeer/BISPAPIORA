using BISPAPIORA.Models.DTOS.GroupPermissionDTO;
using BISPAPIORA.Models.DTOS.TransactionDTO;
using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.CitizenComplianceDTO
{
    public class CitizenComplianceDTO
    {
        public double startingBalanceOnQuarterlyBankStatement { get; set; } = 0.0;
        public double closingBalanceOnQuarterlyBankStatement { get; set; } = 0.0;
        public double citizenComplianceActualSavingAmount { get; set; } = 0.0;
        public int quarterCode { get; set; } = 0;
        public string fkCitizen { get; set; } = string.Empty;
        public bool isCompliant { get; set; } = true;
    }
    public class AddCitizenComplianceDTO : CitizenComplianceDTO
    {
        [Required]
        public new double startingBalanceOnQuarterlyBankStatement { get; set; } = 0.0;
        [Required]
        public new double closingBalanceOnQuarterlyBankStatement { get; set; } = 0.0;
        [Required]
        public new double citizenComplianceActualSavingAmount { get; set; } = 0.0;
        [Required]
        public new string fkCitizen { get; set; } = string.Empty;
        [Required]
        public new int quarterCode { get; set; } = 0;
        public List<AddTransactionDTO> transactionDTO { get; set; } = new List<AddTransactionDTO>();
    }
    public class UpdateCitizenComplianceDTO : CitizenComplianceDTO
    {
        [Required]
        public string citizenComplianceId { get; set; } = string.Empty;
    }
    public class CitizenComplianceResponseDTO : CitizenComplianceDTO
    {
        public string citizenComplianceId { get; set; } = string.Empty;
        public string citizenComplianceStartingMonth { get; set; } = string.Empty;
        public double citizenComplianceCommittedSavingAmount { get; set; } = 0.0;
        public List<TransactionResponseDTO> transactions { get; set; } = new List<TransactionResponseDTO>();
        public int citizenComplianceCode { get; set; } = default!;
    }
}
