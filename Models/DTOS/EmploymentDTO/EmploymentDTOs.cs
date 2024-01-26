using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.EmploymentDTO
{
    public class EmploymentDTO
    {
        public string employmentName { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
    }
    public class AddEmploymentDTO : EmploymentDTO
    {
        [Required]
        public new string employmentName { get; set; } = string.Empty;
    }
    public class UpdateEmploymentDTO : EmploymentDTO
    {
        [Required]
        public string employmentId { get; set; } = string.Empty;
    }
    public class EmploymentResponseDTO : EmploymentDTO
    {
        public string employmentId { get; set; } = string.Empty;
    }
}
