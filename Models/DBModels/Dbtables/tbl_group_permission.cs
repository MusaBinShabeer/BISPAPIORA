using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_group_permission
{
    public Guid group_permission_id { get; set; } = Guid.NewGuid();
    public bool? can_create { get; set; } = true;
    public bool? can_delete { get; set; } = true;
    public bool? can_read { get; set; } = true;
    public bool? can_update { get; set; } = true;
    public Guid? fk_user_type { get; set; } = default!;
    public Guid? fk_functionality { get; set; } = default!;
    public virtual tbl_user_type? tbl_user_type { get; set; } = default!;
    public virtual tbl_functionality? tbl_functionality { get; set; } = default!;
}