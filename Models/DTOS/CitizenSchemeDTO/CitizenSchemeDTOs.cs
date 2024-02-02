using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.CitizenSchemeDTO
{
    public class CitizenSchemeDTO
    {
        public string citizenSchemeYear { get; set; } = string.Empty;
        public string citizenSchemeQuarter { get; set; } = string.Empty;
        public string citizenSchemeStartingMonth { get; set; } = string.Empty;
        public double citizenSchemeSavingAmount { get; set; } = 0.0;
        public string quarterCode { get; set; } = string.Empty;
        public string fkCitizen { get; set; } = string.Empty;
    }
    public class AddCitizenSchemeDTO : CitizenSchemeDTO
    {
        [Required]
        public new string citizenSchemeYear { get; set; } = string.Empty;
        [Required]
        public new string citizenSchemeQuarter { get; set; } = string.Empty;
        [Required]
        public new string citizenSchemeStartingMonth { get; set; } = string.Empty;
        [Required]
        public new double citizenSchemeSavingAmount { get; set; } = 0.0;
        [Required]
        public new string quarterCode { get; set; } = string.Empty;
        [Required]
        public new string fkCitizen { get; set; } = string.Empty;
    }
    public class UpdateCitizenSchemeDTO : CitizenSchemeDTO
    {
        [Required]
        public string citizenSchemeId { get; set; } = string.Empty;
    }
    public class CitizenSchemeResponseDTO : CitizenSchemeDTO
    {
        public string citizenSchemeId { get; set; } = string.Empty;
    }
}
