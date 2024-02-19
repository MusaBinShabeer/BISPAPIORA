using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_image_citizen_attachment
{
    public Guid id { get; set; } = Guid.NewGuid();
    public decimal? code { get; set; } = 0;

    public string? name { get; set; } = string.Empty;

    public byte[]? data { get; set; }

    public string? content_type { get; set; } = string.Empty;

    public string? cnic { get; set; } = string.Empty;

    public Guid? fk_citizen { get; set; } = default!;
}
