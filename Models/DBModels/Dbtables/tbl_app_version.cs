using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_app_version
{
    public Guid app_version_id { get; set; } = Guid.NewGuid();
    public string? app_version { get; set; } = string.Empty;

    public string? app_update_url { get; set; } = string.Empty;
}
