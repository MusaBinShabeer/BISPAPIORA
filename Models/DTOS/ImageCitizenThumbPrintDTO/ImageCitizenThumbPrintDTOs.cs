using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.ImageCitizenThumbPrintDTO
{
    public class ImageCitizenThumbPrintDTO
    {
        public string imageCitizenThumbPrintName { get; set; } = string.Empty;
        public byte[] imageCitizenThumbPrintData { get; set; }
        public string imageCitizenThumbPrintContentType { get; set; } = string.Empty;
        public string imageCitizenThumbPrintCnic { get; set; } = string.Empty;
        public string fkCitizen { get; set; } = string.Empty;
    }
    public class AddImageCitizenThumbPrintDTO : ImageCitizenThumbPrintDTO
    {
        [Required]
        public new string imageCitizenThumbPrintName { get; set; } = string.Empty;
        [Required]
        public new byte[] imageCitizenThumbPrintData { get; set; }
    }
    public class UpdateImageCitizenThumbPrintDTO : ImageCitizenThumbPrintDTO
    {
        [Required]
        public string imageCitizenThumbPrintId { get; set; } = string.Empty;
    }
    public class ImageCitizenThumbPrintResponseDTO : ImageCitizenThumbPrintDTO
    {
        public string imageCitizenThumbPrintId { get; set; } = string.Empty;
        public string citizenName { get; set; } = string.Empty;
        public string citizenCnic { get; set; } = string.Empty;
    }
}
