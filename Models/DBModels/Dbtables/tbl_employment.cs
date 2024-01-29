using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_employment
{
    public Guid employment_id { get; set; } = Guid.NewGuid();

    public string? employment_name { get; set; } = string.Empty;

    public bool? is_active { get; set; } = true;

    public virtual ICollection<tbl_citizen> tbl_citizens { get; set; } = new List<tbl_citizen>();
}
