using System;
using System.Collections.Generic;
using BISPAPIORA.Extensions;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_user
{
    public Guid user_id { get; set; } = Guid.NewGuid();

    public string? user_name { get; set; } = string.Empty;

    public string? user_email { get; set; } = string.Empty;

    public string? user_password { get; set; } = new OtherServices().encodePassword("12345");

    public decimal? user_otp { get; set; } = 0;

    public bool? is_ftp_set { get; set; } = false;          //ftp => first-time Passwword

    public bool? is_active { get; set; } = true;

    public string? user_token { get; set; } = string.Empty;

    public DateTime? insertion_date { get; set; }

    public Guid? fk_user_type { get; set; } = default!;

    public virtual tbl_user_type? tbl_user_type { get; set; }

    public ICollection<tbl_registration>? tbl_registered_citizens { get; set; }

    public ICollection<tbl_enrollment>? tbl_enrolled_citizens { get; set; }

    public ICollection<tbl_citizen_compliance>? tbl_citizen_compliances { get; set; }
}
