using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_citizen_thumb_print
{
    public Guid citizen_thumb_print_id { get; set; } = Guid.NewGuid();

    public string citizen_thumb_print_name { get; set; } = null!;

    public string citizen_thumb_print_path { get; set; } = null!;

    public Guid? fk_citizen { get; set; } = default!;
    public virtual tbl_citizen? tbl_citizen { get; set; } = default!;
}
