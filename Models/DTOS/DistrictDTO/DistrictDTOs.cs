using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.DistrictDTO
{
    public class DistrictDTO
    {
        public string districtName { get; set; } = string.Empty;
        public string fkProvince { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
    }
    public class AddDistrictDTO : DistrictDTO
    {
        [Required]
        public new string districtName { get; set; } = string.Empty;
        [Required]
        public new string fkProvince { get; set; } = string.Empty;
    }
    public class UpdateDistrictDTO : DistrictDTO
    {
        [Required]
        public string districtId { get; set; } = string.Empty;
    }
    public class DistrictResponseDTO : DistrictDTO
    {
        public string districtId { get; set; } = string.Empty;
        public string provinceName { get; set; } = string.Empty;
    }
}
