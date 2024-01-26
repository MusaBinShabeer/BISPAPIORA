using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DBModels.Dbtables
{
    public class tbl_province
    {
        [Key]
        public Guid province_id { get; set; } = Guid.NewGuid();
        public string province_name { get; set; } = string.Empty;
        public bool is_active { get; set; } = true;
        public IEnumerable<tbl_district> tbl_districts { get; set; } = default!;
    }
}
