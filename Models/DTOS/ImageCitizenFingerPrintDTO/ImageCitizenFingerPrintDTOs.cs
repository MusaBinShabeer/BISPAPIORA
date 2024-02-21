using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.ImageCitizenFingerPrintDTO
{
    public class ImageCitizenFingerPrintDTO
    {
        public string imageCitizenFingerPrintName { get; set; } = string.Empty;
        public byte[] imageCitizenFingerPrintData { get; set; }
        public string imageCitizenFingerPrintContentType { get; set; } = string.Empty;
        public string imageCitizenThumbPrintName { get; set; } = string.Empty;
        public byte[] imageCitizenThumbPrintData { get; set; }
        public string imageCitizenThumbPrintContentType { get; set; } = string.Empty;
        public string imageCitizenFingerPrintCnic { get; set; } = string.Empty;
        public string fkCitizen { get; set; } = string.Empty;
    }
    public class AddImageCitizenFingerPrintDTO : ImageCitizenFingerPrintDTO
    {
        [Required]
        public new string imageCitizenFingerPrintName { get; set; } = string.Empty;
        [Required]
        public new byte[] imageCitizenFingerPrintData { get; set; }
        [Required]
        public new string imageCitizenThumbPrintName { get; set; } = string.Empty;
        [Required]
        public new byte[] imageCitizenThumbPrintData { get; set; }
    }
    public class UpdateImageCitizenFingerPrintDTO : ImageCitizenFingerPrintDTO
    {
        [Required]
        public string imageCitizenFingerPrintId { get; set; } = string.Empty;
    }
    public class ImageCitizenFingerPrintResponseDTO : ImageCitizenFingerPrintDTO
    {
        public string imageCitizenFingerPrintId { get; set; } = string.Empty;
        public string citizenName { get; set; } = string.Empty;
        public string citizenCnic { get; set; } = string.Empty;
        public int imageCitizenFingerPrintCode { get; set; } = default!;
    }
}
