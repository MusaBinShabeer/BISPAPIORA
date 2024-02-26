using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_user
{
    public Guid user_id { get; set; } = Guid.NewGuid();

    public string? user_name { get; set; } = string.Empty;

    public string? user_email { get; set; } = string.Empty;

    public string? user_password { get; set; } = string.Empty;

    public decimal? user_otp { get; set; } = 0;

    public bool? is_active { get; set; } = true;

    public string? user_token { get; set; } = string.Empty;

    public Guid? fk_user_type { get; set; } = default!;

    public virtual tbl_user_type? tbl_user_type { get; set; }
}
