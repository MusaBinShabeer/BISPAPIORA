using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_citizen_attachment
{
    public Guid citizen_attachment_id { get; set; } = Guid.NewGuid();

    public string attachment_name { get; set; } = null!;

    public string attachment_path { get; set; } = null!;

    public Guid? fk_citizen { get; set; } = default!;
    public virtual tbl_citizen? tbl_citizen { get; set; } = default!;
}
