using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_province
{
    public Guid province_id { get; set; } = Guid.NewGuid();

    public string? province_name { get; set; } = string.Empty;

    public bool? is_active { get; set; } = true;

    public virtual ICollection<tbl_district> tbl_districts { get; set; } = new List<tbl_district>();
}
