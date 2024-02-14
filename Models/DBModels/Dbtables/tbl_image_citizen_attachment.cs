using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_image_citizen_attachment
{
    public Guid id { get; set; }

    public string? name { get; set; }

    public byte[]? data { get; set; }

    public string? content_type { get; set; }

    public string? cnic { get; set; }

    public Guid? fk_citizen { get; set; }
}
