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
    }
}
