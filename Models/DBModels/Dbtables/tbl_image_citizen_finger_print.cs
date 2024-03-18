using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_image_citizen_finger_print
{
    public Guid id { get; set; } = Guid.NewGuid();

    public string? finger_print_name { get; set; } = string.Empty;

    public byte[]? finger_print_data { get; set; }

    public string? finger_print_content_type { get; set; } = string.Empty;
    public string? thumb_print_name { get; set; } = string.Empty;

    public byte[]? thumb_print_data { get; set; }

    public string? thumb_print_content_type { get; set; } = string.Empty;

    public string? cnic { get; set; } = string.Empty;

    public Guid? fk_citizen { get; set; } = default!;

    public virtual tbl_citizen? tbl_citizen { get; set; }
}