using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_citizen_scheme
{
    public Guid citizen_scheme_id { get; set; } = Guid.NewGuid();

    public string? citizen_scheme_year { get; set; } = string.Empty;

    public string? citizen_scheme_quarter { get; set; } = string.Empty;

    public string? citizen_scheme_starting_month { get; set; } = string.Empty;

    public decimal? citizen_scheme_saving_amount { get; set; } = 0;

    public Guid? fk_citizen { get; set; } = new Guid();

    public virtual tbl_citizen? tbl_citizen { get; set; }
    public int? citizen_scheme_quarter_code { get; set; }
}
