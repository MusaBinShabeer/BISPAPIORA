using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.ProvinceStatusResponseDTO
{
    public class ProvinceStatusResponseDTO
    {
        public string provinceName { get; set; } = string.Empty;
        public int applicantCount { get; set; } = 0;
    }
}
