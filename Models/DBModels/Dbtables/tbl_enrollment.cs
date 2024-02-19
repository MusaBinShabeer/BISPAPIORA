using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_enrollment
{
    public Guid enrollment_id { get; set; } = Guid.NewGuid();

    public Guid? fk_citizen { get; set; } = default(Guid?);
    public decimal? citizen_code { get; set; } = 0;

    public virtual tbl_citizen? tbl_citizen { get; set; }
}
