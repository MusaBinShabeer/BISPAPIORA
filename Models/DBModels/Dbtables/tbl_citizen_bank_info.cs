using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_citizen_bank_info
{
    public Guid citizen_bank_info_id { get; set; } = Guid.NewGuid();

    public string? iban_no { get; set; } = string.Empty;

    public string? account_type { get; set; } =string.Empty;

    public string? account_holder_name { get; set; } = string.Empty;

    public decimal? family_income { get; set; } = 0;

    public bool? family_saving_account { get; set; } = false;

    public Guid? fk_bank { get; set; } = default!;

    public Guid? fk_citizen { get; set; } = default!;

    public virtual tbl_bank? tbl_bank { get; set; } = default!;
    public virtual tbl_bank_other_specification? tbl_bank_other_specification { get; set; } = default!;

    public virtual tbl_citizen? tbl_citizen { get; set; }=default!;
}
