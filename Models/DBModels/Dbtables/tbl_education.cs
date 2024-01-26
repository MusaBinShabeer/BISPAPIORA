using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DBModels.Dbtables
{
    public class tbl_education
    {
        [Key]
        public Guid education_id { get; set; } = Guid.NewGuid();
        public string education_name { get; set; } = string.Empty;
        public IEnumerable<tbl_citizen> citizens { get; set; } = default!;
        public bool is_active { get; set; } = true;
    }
}
