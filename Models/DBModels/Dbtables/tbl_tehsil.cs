using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DBModels.Dbtables
{
    public class tbl_tehsil
    {
        [Key]
        public Guid tehsil_id { get; set; } = Guid.NewGuid();
        public string tehsil_name { get; set; } = string.Empty;
        public bool is_active { get; set; } = true;
        public Guid fk_district { get; set; } = default!;
        public tbl_district tbl_district { get; set; } = default!;
        public IEnumerable<tbl_citizen> citizens { get; set; } = default!;
    }
}
