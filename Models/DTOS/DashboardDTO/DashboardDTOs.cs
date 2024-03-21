namespace BISPAPIORA.Models.DTOS.DashboardDTO
{
    public class DashboardCitizenCountPercentageDTO
    {
        public double registeredPercentage { get; set; } = 0;
        public double enrolledPercentage { get; set; } = 0;
    }
    public class DashboardProvinceCitizenCountPercentageDTO
    {
        public string provinceName { get; set; } = string.Empty;
        public double citizenPercentage { get; set; } = 0;
    }
    public class DashboardDistrictCitizenCountPercentageDTO : DashboardProvinceCitizenCountPercentageDTO
    {
        public string districtName { get; set; } = string.Empty;
    }
    public class DashboardTehsilCitizenCountPercentageDTO : DashboardDistrictCitizenCountPercentageDTO
    {
        public string tehsilName { get; set; } = string.Empty;
    }
    public class DashboardUserPerformanceResponseDTO
    {
        public int registeredCount { get; set; } = 0;
        public int enrolledCount { get; set; } = 0;
    }
    public class DashboardCitizenBaseModel
    {
        public Guid citizen_id { get; set; }
        public string citizen_name { get; set; } = string.Empty;
        public string user_name { get; set; } = string.Empty;
        public Guid? registered_by { get; set; } = default!;
        public DateTime? registered_date { get; set; } = default!;
        public Guid? enrolled_by { get; set; } = default!;
        public DateTime? enrolled_date { get; set; } = default!;
    }
    public class DashboardCitizenLocationModel : DashboardCitizenBaseModel
    {
        public Guid province_id { get; set; }
        public string province_name { get; set; } = string.Empty;
        public string district_name { get; set; } = string.Empty;
        public Guid? district_id { get; set; } = default!;
        public string tehsil_name { get; set; } = string.Empty;
        public Guid? tehsil_id { get; set; } = default!;
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
