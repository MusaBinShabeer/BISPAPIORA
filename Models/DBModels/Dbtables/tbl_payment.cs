using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_payment
{
    public Guid payment_id { get; set; } = Guid.NewGuid();

    public decimal? paid_amount { get; set; } = default;

    public decimal? due_amount { get; set; } = default;

    public int? payment_quarter_code { get; set; } = 0;

    public Guid? fk_compliance { get; set; } = null;

    public Guid? fk_citizen { get; set; } = default!;

    public virtual tbl_citizen_compliance? tbl_citizen_compliance { get; set; }

    public virtual tbl_citizen? tbl_citizen { get; set; }
}