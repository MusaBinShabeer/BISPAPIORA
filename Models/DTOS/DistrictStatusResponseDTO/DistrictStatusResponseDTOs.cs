using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.DistrictStatusResponseDTO
{
    public class DistrictStatusResponseDTO
    {
        public string districtName { get; set; } = string.Empty;
        public int applicantCount { get; set; } = 0;
    }
}
