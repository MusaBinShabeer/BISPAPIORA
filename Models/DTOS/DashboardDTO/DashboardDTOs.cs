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
    public class WebDashboardStats
    {
        public string StatName { get; set; } = string.Empty;
        public double StatCount { get; set; } = 0;
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
    public class CitizenBaseModel
    {
        public Guid citizen_id { get; set; } = Guid.NewGuid();

        public string citizen_cnic { get; set; } = string.Empty;

        public decimal id { get; set; }

        public string? citizen_name { get; set; } = string.Empty;

        public string? citizen_father_spouce_name { get; set; } = string.Empty;

        public string? citizen_phone_no { get; set; } = string.Empty;

        public string? citizen_gender { get; set; } = string.Empty;

        public string? citizen_address { get; set; } = string.Empty;

        public string? citizen_martial_status { get; set; } = string.Empty;

        public DateTime? citizen_date_of_birth { get; set; } = default(DateTime?);

        public Guid? fk_citizen_education { get; set; } = default(Guid?);

        public Guid? fk_citizen_employment { get; set; } = default(Guid?);

        public Guid? fk_tehsil { get; set; } = default(Guid?);

        public virtual tbl_education? tbl_citizen_education { get; set; }

        public virtual tbl_employment? tbl_citizen_employment { get; set; }

        public virtual tbl_tehsil? tbl_citizen_tehsil { get; set; }

        public virtual tbl_citizen_bank_info? tbl_citizen_bank_info { get; set; }

        public virtual tbl_citizen_family_bank_info? tbl_citizen_family_bank_info { get; set; }

        public virtual tbl_citizen_scheme? tbl_citizen_scheme { get; set; }

        public virtual tbl_citizen_compliance? tbl_citizen_compliance { get; set; }

        public virtual tbl_enrollment? tbl_enrollment { get; set; }

        public virtual tbl_registration? tbl_citizen_registration { get; set; }
        //public virtual tbl_citizen_attachment? tbl_citizen_attachment { get; set; }
        //public virtual tbl_citizen_thumb_print? tbl_citizen_thumb_print { get; set; }
        public virtual tbl_employment_other_specification? tbl_employment_other_specification { get; set; }

        public virtual tbl_image_citizen_attachment? tbl_image_citizen_attachment { get; set; }

        public virtual tbl_image_citizen_finger_print? tbl_image_citizen_finger_print { get; set; }

        public virtual ICollection<tbl_transaction> tbl_transactions { get; set; } = new List<tbl_transaction>();

        public bool? is_valid_beneficiary { get; set; }

        public decimal? unique_hh_id { get; set; } // Unique HouseholdId

        public DateTime? submission_date { get; set; } // through form submission date

        public string? pmt { get; set; } // poverty score 40 =< eligible

        public DateTime? insertion_date { get; set; }
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
}
  
