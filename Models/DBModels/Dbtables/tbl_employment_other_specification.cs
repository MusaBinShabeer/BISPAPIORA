using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_employment_other_specification
{
    public Guid employment_other_specification_id { get; set; } = Guid.NewGuid();
    public decimal? code { get; set; } = 0;
    public string? employment_other_specification { get; set; } = string.Empty;
    public Guid? fk_citizen { get; set; } = default!;
    public virtual tbl_citizen? tbl_citizen { get; set; } = default!;
}
