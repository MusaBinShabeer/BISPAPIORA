using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_tehsil
{
    public Guid tehsil_id { get; set; } = Guid.NewGuid();

    public string? tehsil_name { get; set; } = string.Empty;

    public bool? is_active { get; set; } = true;

    public Guid? fk_district { get; set; } = new Guid();

    public virtual tbl_district? tbl_district { get; set; }

    public int? tehsil_code { get; set; } = default(int?);
    public int? district_code { get; set; } = default(int?);

    public virtual ICollection<tbl_citizen> tbl_citizens { get; set; } = new List<tbl_citizen>();
}
