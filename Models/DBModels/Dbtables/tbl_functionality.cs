using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_functionality
{
    public Guid functionality_id { get; set; } = Guid.NewGuid();
    public string? functionality_name { get; set; } = string.Empty;
    public bool? is_active { get; set; } = true;
    public virtual ICollection<tbl_group_permission> tbl_group_permissions { get; set; } = new List<tbl_group_permission>();
}
