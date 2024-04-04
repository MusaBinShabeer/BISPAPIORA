using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_transaction
{
    public Guid transaction_id { get; set; } = Guid.NewGuid();
   
    public DateTime? transaction_date { get; set; } = default(DateTime?);

    public string? transaction_type { get; set; }= string.Empty;

    public decimal? transaction_amount { get; set; } = default;

    public int? transaction_quarter_code { get; set; } = 0;

    public Guid? fk_citizen { get; set; } = default(Guid?);

    public virtual tbl_citizen? tbl_citizen { get; set; } = default!;
}
