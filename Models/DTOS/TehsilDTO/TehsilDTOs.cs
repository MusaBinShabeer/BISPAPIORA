using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.TehsilDTO
{
    public class TehsilDTO
    {
        public string tehsilName { get; set; } = string.Empty;
        public string fkDistrict { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
    }
    public class AddTehsilDTO : TehsilDTO
    {
        [Required]
        public new string tehsilName { get; set; } = string.Empty;
        [Required]
        public new string fkDistrict { get; set; } = string.Empty;
    }
    public class UpdateTehsilDTO : TehsilDTO
    {
        [Required]
        public string tehsilId { get; set; } = string.Empty;
    }
    public class TehsilResponseDTO : TehsilDTO
    {
        public string tehsilId { get; set; } = string.Empty;
        public string provinceId { get; set; } = string.Empty;
        public string provinceName { get; set; } = string.Empty;
        public string districtName { get; set; } = string.Empty;
    }
}
