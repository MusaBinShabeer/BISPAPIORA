using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_citizen
{
    public Guid citizen_id { get; set; } = Guid.NewGuid();

    public string citizen_cnic { get; set; } = string.Empty;

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

    public virtual tbl_citizen_scheme? tbl_citizen_scheme { get; set; }
    //public virtual tbl_citizen_compliance? tbl_citizen_compliance { get; set; }

    public virtual tbl_enrollment? tbl_enrollment { get; set; }

    public virtual tbl_registration? tbl_citizen_registration { get; set; }
    public virtual tbl_citizen_attachment? tbl_citizen_attachment { get; set; }
    public virtual tbl_citizen_thumb_print? tbl_citizen_thumb_print { get; set; }
    //public virtual ICollection<tbl_transaction> tbl_transactions { get; set; } = new List<tbl_transaction>();
    public decimal? is_valid_beneficiary { get; set; }

    public decimal? unique_hh_id { get; set; } // Unique HouseholdId

    public DateTime? submission_date { get; set; } // through form submission date

    public string? pmt { get; set; } // poverty score 40 =< eligible
}
