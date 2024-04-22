using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_citizen_compliance
{
    public Guid citizen_compliance_id { get; set; } = Guid.NewGuid();
 
    public decimal? starting_balance_on_quarterly_bank_statement { get; set; } = 0;

    public decimal? closing_balance_on_quarterly_bank_statement { get; set; } = 0;

    public decimal? citizen_compliance_actual_saving_amount { get; set; } = 0;

    public int? citizen_compliance_quarter_code { get; set; } = 0;

    public bool? is_compliant { get; set; } = false;

    public Guid? fk_citizen { get; set; } = new Guid();
    public virtual tbl_citizen? tbl_citizen { get; set; }

    public virtual tbl_bank_statement? tbl_bank_statement { get; set; }

    public virtual ICollection<tbl_transaction>? tbl_transactions { get; set; }

    public virtual ICollection<tbl_payment> tbl_payments { get; set; } = new List<tbl_payment>();

}
