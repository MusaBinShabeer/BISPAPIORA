using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DBModels.Dbtables
{
    public class tbl_bank
    {
        [Key]
        public Guid bank_id { get; set; } = Guid.NewGuid();
        public string bank_name { get; set; } = string.Empty;
        public bool is_active { get; set; } = true;
        public tbl_citizen_bank_info? tbl_citizen_bank_info { get; set; } = default!;
    }
}
