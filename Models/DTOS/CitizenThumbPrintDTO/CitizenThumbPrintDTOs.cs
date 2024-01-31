using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.CitizenThumbPrintDTO
{
    public class CitizenThumbPrintDTO
    {
        public string citizenThumbPrintName { get; set; } = string.Empty;
        public string citizenThumbPrintPath { get; set; } = string.Empty;
        public string fkCitizen { get; set; } = string.Empty;
    }
    public class AddCitizenThumbPrintDTO : CitizenThumbPrintDTO
    {
        [Required]
        public new string citizenThumbPrintName { get; set; } = string.Empty;
        [Required]
        public new string citizenThumbPrintPath { get; set; } = string.Empty;
    }
    public class UpdateCitizenThumbPrintDTO : CitizenThumbPrintDTO
    {
        [Required]
        public string citizenThumbPrintId { get; set; } = string.Empty;
    }
    public class CitizenThumbPrintResponseDTO : CitizenThumbPrintDTO
    {
        public string citizenThumbPrintId { get; set; } = string.Empty;
        public string citizenName { get; set; } = string.Empty;
        public string citizenCnic { get; set; } = string.Empty;
    }
}
