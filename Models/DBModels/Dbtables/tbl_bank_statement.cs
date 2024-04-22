﻿using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_bank_statement
{
    public Guid id { get; set; } = Guid.NewGuid();

    public string? name { get; set; } = string.Empty;

    public byte[]? data { get; set; }

    public string? content_type { get; set; } = string.Empty;

    public string? cnic { get; set; } = string.Empty;

    public DateTime? insertion_date { get; set; }

    public Guid? fk_citizen_compliance { get; set; } = default!;

    public virtual tbl_citizen_compliance? tbl_citizen_compliance { get; set; }
}
