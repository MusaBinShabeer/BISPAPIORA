namespace BISPAPIORA.Models.DTOS.DashboardDTO
{
    public class DashboardUserPerformanceResponseDTO
    {
        public int registeredCount { get; set; } = 0;
        public int enrolledCount { get; set; } = 0;
    }
    public class DashboardDTO : DashboardUserPerformanceResponseDTO
    {
        public int totalCitizenCount { get; set; } = 0;
    }
    public class TehsilStatusResponseDTO : DashboardDTO
    {
        public string tehsilName { get; set; } = string.Empty;
    }
    public class DistrictStatusResponseDTO : DashboardDTO
    {
        public string districtName { get; set; } = string.Empty;
    }
    public class ProvinceStatusResponseDTO : DashboardDTO
    {
        public string provinceName { get; set; } = string.Empty;
    }
}
