using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_bank_other_specification
{
    public Guid bank_other_specification_id { get; set; } = Guid.NewGuid();
    public string? bank_other_specification { get; set; } = string.Empty;
    public Guid? fk_citizen_bank_info { get; set; } = default!;
    public Guid? fk_citizen_family_bank_info { get; set; } = default!;
    public virtual tbl_citizen_bank_info? tbl_citizen_bank_info { get; set; } = default!;
    public virtual tbl_citizen_family_bank_info? tbl_citizen_family_bank_info { get; set; } = default!;
}
