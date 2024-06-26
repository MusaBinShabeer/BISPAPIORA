﻿using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_bank
{
    public Guid bank_id { get; set; } = Guid.NewGuid();
    public string? bank_name { get; set; } = string.Empty;
    public bool? is_active { get; set; } = true;

    public string? bank_prefix_iban { get; set; } = string.Empty;

    public virtual ICollection<tbl_citizen_bank_info> tbl_citizen_bank_infos { get; set; } = new List<tbl_citizen_bank_info>();
    public virtual ICollection<tbl_citizen_family_bank_info> tbl_citizen_family_bank_infos { get; set; } = new List<tbl_citizen_family_bank_info>();
}
