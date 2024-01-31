using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.CitizenAttachmentDTO
{
    public class CitizenAttachmentDTO
    {
        public string citizenAttachmentName { get; set; } = string.Empty;
        public string citizenAttachmentPath { get; set; } = string.Empty;
        public string fkCitizen { get; set; } = string.Empty;
    }
    public class AddCitizenAttachmentDTO : CitizenAttachmentDTO
    {
        [Required]
        public new string citizenAttachmentName { get; set; } = string.Empty; 
        [Required]
        public new string citizenAttachmentPath { get; set; } = string.Empty;
    }
    public class UpdateCitizenAttachmentDTO : CitizenAttachmentDTO
    {
        [Required]
        public string citizenAttachmentId { get; set; } = string.Empty;
    }
    public class CitizenAttachmentResponseDTO : CitizenAttachmentDTO
    {
        public string citizenAttachmentId { get; set; } = string.Empty;
        public string citizenName { get; set; } = string.Empty;
        public string citizenCnic { get; set; } = string.Empty;
    }
}
