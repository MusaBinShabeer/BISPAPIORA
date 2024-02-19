using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_district
{
    public Guid district_id { get; set; } = Guid.NewGuid();
    public string? district_name { get; set; } = string.Empty;
    public bool? is_active { get; set; } = true;
    public Guid? fk_province { get; set; } = default(Guid?);
    public virtual tbl_province? tbl_province { get; set; }
    public int? district_code { get; set; } = default!;
    public int? province_code { get; set; } = default!;
    public virtual ICollection<tbl_tehsil> tbl_tehsils { get; set; } = new List<tbl_tehsil>();
}
