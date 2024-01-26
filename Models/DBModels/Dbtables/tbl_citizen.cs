using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DBModels.Dbtables
{
    public class tbl_citizen
    {
        [Key]
        public Guid citizen_id { get; set; } = Guid.NewGuid();
        public string citizen_cnic { get; set; } = string.Empty;
        public string citizen_name { get; set; } = string.Empty;
        public string citizen_father_spouce_name { get; set; } = string.Empty;
        public string citizen_phone_no { get; set; } = string.Empty;
        public string citizen_gender { get; set; } = string.Empty;
        public string citizen_address { get; set; } = string.Empty;
        public string citizen_martial_status { get; set; } = string.Empty;
        public DateTime? citizen_date_of_birth { get; set; } = null;
        public Guid? fk_citizen_education { get; set; } = default!;
        public Guid? fk_citizen_employment { get; set; } = default!;
        public Guid? fk_tehsil { get; set; } = default!;
        public tbl_tehsil? citizen_tehsil { get; set; } = default!;
        public tbl_education? citizen_education { get; set; } = default!;
        public tbl_employment? citizen_employement { get; set; } = default!;
        public tbl_registration? citizen_registration { get; set; } = default!;
        public tbl_citizen_bank_info? tbl_citizen_bank_info { get; set; } = default!;
        public tbl_citizen_scheme? tbl_citizen_scheme { get; set; } = default!;
    }
}
