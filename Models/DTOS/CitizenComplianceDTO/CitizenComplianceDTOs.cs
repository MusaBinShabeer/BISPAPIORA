using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.CitizenComplianceDTO
{
    public class CitizenComplianceDTO
    {
        public double startingBalanceOnQuarterlyBankStatement { get; set; } = 0.0;
        public double closingBalanceOnQuarterlyBankStatement { get; set; } = 0.0;
        public double citizenComplianceActualSavingAmount { get; set; } = 0.0;
        public string fkCitizen { get; set; } = string.Empty;
        public string fkCitizenScheme { get; set; } = string.Empty;
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
        public new string fkCitizenScheme { get; set; } = string.Empty;
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
    }
}
