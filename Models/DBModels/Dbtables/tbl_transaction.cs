using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_transaction
{
    public Guid transaction_id { get; set; }
    public string? transaction_date { get; set; } = string.Empty;
    public string? transaction_type { get; set; } = string.Empty;
    public decimal? transaction_ammount { get; set; } = 0;
    public Guid? fk_citizen { get; set; } = default!;
    public virtual tbl_citizen? tbl_citizen { get; set; } = default!;
}
