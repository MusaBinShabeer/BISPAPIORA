using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.AppVersionDTO
{
    public class AppVersionDTO
    {
        public string appVersion { get; set; } = string.Empty;
        public string appVersionURL { get; set; } = string.Empty;
    }
    public class AddAppVersionDTO : AppVersionDTO
    {
        [Required]
        public new string appVersion { get; set; } = string.Empty; 
        [Required]
        public new string appVersionURL { get; set; } = string.Empty;
    }
    public class UpdateAppVersionDTO : AppVersionDTO
    {
        [Required]
        public string appVersionId { get; set; } = string.Empty;
    }
    public class AppVersionResponseDTO : AppVersionDTO
    {
        public string appVersionId { get; set; } = string.Empty;
    }
}
