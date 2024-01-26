using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.ProvinceDTO
{
    public class ProvinceDTO
    {
        public string provinceName { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
    }
    public class AddProvinceDTO : ProvinceDTO
    {
        [Required]
        public new string provinceName { get; set; } = string.Empty;
    }
    public class UpdateProvinceDTO : ProvinceDTO
    {
        [Required]
        public string provinceId { get; set; } = string.Empty;
    }
    public class ProvinceResponseDTO : ProvinceDTO
    {
        public string provinceId { get; set; } = string.Empty;
    }
}
