using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_bank
{
    public Guid bank_id { get; set; }

    public string? bank_name { get; set; }

    public bool? is_active { get; set; }

    public string? bank_prefix_iban { get; set; }
    public virtual ICollection<tbl_citizen_bank_info> tbl_citizen_bank_infos { get; set; } = new List<tbl_citizen_bank_info>();
}
