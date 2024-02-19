using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.CitizenBankInfoDTO
{
    public class CitizenBankInfoDTO
    {
        public string fkCitizen { get; set; } = string.Empty;
        public string fkBank { get; set; } = string.Empty;
        public string ibanNo { get; set; } = string.Empty;
    }
    public class AddEnrolledCitizenBankInfoDTO : CitizenBankInfoDTO
    {
        [Required]
        public new string fkCitizen { get; set; } = string.Empty;
        [Required]
        public new string fkBank { get; set; } = string.Empty;
        [Required]
        public new string ibanNo { get; set; } = string.Empty;
        [Required]
        public int accountType { get; set; } = 0;
    }
    public class AddRegisteredCitizenBankInfoDTO : CitizenBankInfoDTO
    {
        [Required]
        public new string fkCitizen { get; set; } = string.Empty;
        [Required]
        public new string fkBank { get; set; } = string.Empty;
        [Required]
        public new string ibanNo { get; set; } = string.Empty;
        [Required]
        public string accountHolderName { get; set; } = string.Empty;
        [Required]
        public double aIOA { get; set; } = 0.0;                         //Average Income Of Account
        [Required]
        public bool familySavingAccount { get; set; } = false;
    }
    public class UpdateEnrolledCitizenBankInfoDTO : CitizenBankInfoDTO
    {
        [Required]
        public string CitizenBankInfoId { get; set; } = string.Empty;
        public int accountType { get; set; } = 0;
    }
    public class UpdateRegisteredCitizenBankInfoDTO : CitizenBankInfoDTO
    {
        [Required]
        public string CitizenBankInfoId { get; set; } = string.Empty;
        public string accountHolderName { get; set; } = string.Empty;
        public double aIOA { get; set; } = 0.0;                         //Average Income Of Account
        public bool familySavingAccount { get; set; } = false;
    }
    public class EnrolledCitizenBankInfoResponseDTO : CitizenBankInfoDTO
    {
        public string CitizenBankInfoId { get; set; } = string.Empty;
        public string accountTypeName { get; set; } = string.Empty;
        public int accountType { get; set; } = 0;
        public string citizenName { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
    }
    public class RegisteredCitizenBankInfoResponseDTO : CitizenBankInfoDTO
    {
        public string CitizenBankInfoId { get; set; } = string.Empty;
        public string citizenName { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string accountHolderName { get; set; } = string.Empty;
        public double aIOA { get; set; } = 0.0;                         //Average Income Of Account
        public bool familySavingAccount { get; set; } = false;
        public int citizenBankInfoCode { get; set; } = default!;
    }
}
