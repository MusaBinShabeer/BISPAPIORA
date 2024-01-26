using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DBModels.Dbtables
{
    public class tbl_district
    {
        [Key]
        public Guid district_id { get; set; } = Guid.NewGuid();
        public string district_name { get; set; } = string.Empty;
        public bool is_active { get; set; } = true;
        public Guid fk_province { get; set; } = Guid.NewGuid();
        public tbl_province tbl_province { get; set; } = default!;
        public IEnumerable<tbl_tehsil> tbl_tehsils { get; set; } = default!;
    }
}
