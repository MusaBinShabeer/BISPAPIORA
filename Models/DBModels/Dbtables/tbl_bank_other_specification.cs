using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_bank_other_specification
{
    public Guid bank_other_specification_id { get; set; } = Guid.NewGuid();
    public string? bank_other_specification { get; set; } = string.Empty;
    public Guid? fk_citizen { get; set; } = default!;
    public virtual tbl_citizen? tbl_citizen { get; set; } = default!;
}
