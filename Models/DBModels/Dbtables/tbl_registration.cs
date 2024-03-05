using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class tbl_registration
{
    public Guid registration_id { get; set; } = Guid.NewGuid();

    public Guid? fk_citizen { get; set; } = default(Guid?);
    public DateTime? registered_date { get; set; } // through form registration date

    public Guid? fk_registered_by { get; set; } = default(Guid?);

    public virtual tbl_citizen? tbl_citizen { get; set; }
    public virtual tbl_user? registerd_by { get; set; }
}
