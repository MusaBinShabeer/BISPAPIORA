using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_image_citizen_thumb_print
{
    public Guid id { get; set; } = Guid.NewGuid();

    public string? name { get; set; } = string.Empty;

    public byte[]? data { get; set; }

    public string? content_type { get; set; } = string.Empty;

    public string? cnic { get; set; } = string.Empty;

    public Guid? fk_citizen { get; set; } = default!;
}
