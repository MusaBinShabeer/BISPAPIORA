using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.FunctionalityDTO
{
    public class FunctionalityDTO
    {
        public string functionalityName { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
    }
    public class AddFunctionalityDTO : FunctionalityDTO
    {
        [Required]
        public new string functionalityName { get; set; } = string.Empty;
    }
    public class UpdateFunctionalityDTO : FunctionalityDTO
    {
        [Required]
        public string functionalityId { get; set; } = string.Empty;
    }
    public class FunctionalityResponseDTO : FunctionalityDTO
    {
        public string functionalityId { get; set; } = string.Empty;
    }
}
