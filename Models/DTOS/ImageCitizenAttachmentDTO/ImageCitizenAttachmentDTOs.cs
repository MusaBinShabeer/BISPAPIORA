using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.ImageCitizenAttachmentDTO
{
    public class ImageCitizenAttachmentDTO
    {
        public string imageCitizenAttachmentName { get; set; } = string.Empty;
        public byte[] imageCitizenAttachmentData { get; set; }
        public string imageCitizenAttachmentContentType { get; set; } = string.Empty;
        public string imageCitizenAttachmentCnic { get; set; } = string.Empty;
        public string fkCitizen { get; set; } = string.Empty;
    }
    public class AddImageCitizenAttachmentDTO : ImageCitizenAttachmentDTO
    {
        [Required]
        public new string imageCitizenAttachmentName { get; set; } = string.Empty;
        [Required]
        public new byte[] imageCitizenAttachmentData { get; set; }
    }
    public class UpdateImageCitizenAttachmentDTO : ImageCitizenAttachmentDTO
    {
        [Required]
        public string imageCitizenAttachmentId { get; set; } = string.Empty;
    }
    public class ImageCitizenAttachmentResponseDTO : ImageCitizenAttachmentDTO
    {
        public string imageCitizenAttachmentId { get; set; } = string.Empty;
        public string citizenName { get; set; } = string.Empty;
        public string citizenCnic { get; set; } = string.Empty;
    }
}
