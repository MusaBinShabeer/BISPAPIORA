using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DBModels.Dbtables
{
    public class tbl_registration
    {
        [Key]
        public Guid registration_id { get; set; } = Guid.NewGuid();
        public Guid fk_citizen { get; set; } = default!;
        public tbl_citizen tbl_citizen { get; set; } = default!;
    }
}
