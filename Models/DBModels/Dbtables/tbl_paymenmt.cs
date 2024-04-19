using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_paymenmt
{
    public Guid paymenmt_id { get; set; } = Guid.NewGuid();
    public string? paymenmt_amount { get; set; } = string.Empty;
    public bool? fk_compliance { get; set; } = true;
    public virtual tbl_citizen_compliance? tbl_citizen_compliance { get; set; }
}
