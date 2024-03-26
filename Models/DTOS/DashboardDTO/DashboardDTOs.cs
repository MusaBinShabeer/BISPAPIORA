using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Models.DTOS.DashboardDTO
{
    public class DashboardCitizenCountPercentageDTO
    {
        public double registeredPercentage { get; set; } = 0;
        public double enrolledPercentage { get; set; } = 0;
    }
    public class DashboardCitizenEducationalPercentageStatDTO
    {
        public string educationalBackground { get; set; } = default!;
        public double educationalBackgroundPercentage { get; set; } = 0;
    }

    public class DashboardCitizenEmploymentPercentageStatDTO
    {
        public string employmentBackground { get; set; } = default!;
        public double employmentBackgroundPercentage { get; set; } = 0;
    }

    public class DashboardCitizenGenderPercentageDTO
    {
        public string citizenGender { get; set; }
        public double citizenGenderPercentage { get; set; }
    }

    public class DashboardCitizenMaritalStatusPercentageDTO
    {
        public string citizenMaritalStatus { get; set; }
        public double citizenMaritalStatusPercentage { get; set; }
    }

    public class DashboardProvinceCitizenCountPercentageDTO
    {
        public string provinceName { get; set; } = string.Empty;
        public double citizenPercentage { get; set; } = 0;
    }

    public class DashboardCitizenCountSavingAmountDTO
    {
        public decimal totalCitizenCount { get; set; } = 0;
        public decimal? savingAmount { get; set; } = 0;
    }

    public class DashboardCitizenTrendDTO
    {
        public decimal totalCitizenCount { get; set; } = 0;
        public DateTime? insertionMonth { get; set; } = default!;
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
        public string educationName { get; set; } = default!;
        public Guid? educationId { get; set; } = default!;
        public string employmentName { get; set; } = default!;
        public Guid? employmentId { get; set; } = default!;

        public string citizen_gender { get; set; } = string.Empty;
        public string citizen_martial_status { get; set; } = string.Empty;

        public decimal? saving_amount { get; set; } = 0;
        public Guid? citizen_scheme_id { get; set; } = default!;

        public DateTime? insertion_date { get; set; } = default!;

        public tbl_enrollment? enrollment { get; set; } = default!;

        public tbl_registration? registration { get; set; } = default!;
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
