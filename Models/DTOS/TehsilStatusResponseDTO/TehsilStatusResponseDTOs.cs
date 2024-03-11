using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.TehsilStatusResponseDTO
{
    public class TehsilStatusResponseDTO
    {
        public string tehsilName { get; set; } = string.Empty;
        public int applicantCount { get; set; } = 0;
    }
}
