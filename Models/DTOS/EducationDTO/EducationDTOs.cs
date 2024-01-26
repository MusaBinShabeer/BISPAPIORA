using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.EducationDTO
{
    public class EducationDTO
    {
        public string educationName { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
    }
    public class AddEducationDTO : EducationDTO
    {
        [Required]
        public new string educationName { get; set; } = string.Empty;
    }
    public class UpdateEducationDTO : EducationDTO
    {
        [Required]
        public string educationId { get; set; } = string.Empty;
    }
    public class EducationResponseDTO : EducationDTO
    {
        public string educationId { get; set; } = string.Empty;
    }
}
