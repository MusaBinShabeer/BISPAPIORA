using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_user_type
{
    public Guid user_type_id { get; set; } = Guid.NewGuid();

    public string? user_type_name { get; set; } = string.Empty;

    public bool? is_active { get; set; } = true;

    public virtual ICollection<tbl_user> tbl_users { get; set; } = new List<tbl_user>();
}
