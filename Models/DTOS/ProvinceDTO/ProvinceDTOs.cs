using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.ProvinceDTO
{
    public class ProvinceDTO
    {
        public string provinceName { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;        
        public int provinceCode { get; set; } = default(int);
    }
    public class AddProvinceDTO : ProvinceDTO
    {
        [Required]
        public new string provinceName { get; set; } = string.Empty;
        [Required]
        public new string provinceCode { get; set; } = string.Empty;
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
