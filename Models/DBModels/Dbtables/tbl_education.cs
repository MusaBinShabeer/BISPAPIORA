using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_education
{
    public Guid education_id { get; set; } = Guid.NewGuid();

    public string? education_name { get; set; } = string.Empty;

    public bool? is_active { get; set; } = true;

    public virtual ICollection<tbl_citizen> tbl_citizens { get; set; } = new List<tbl_citizen>();
}