using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DBModels.Dbtables
{
    public class tbl_citizen_bank_info
    {
        [Key]
        public Guid citizen_bank_info_id { get; set; } = Guid.NewGuid();
        public string iban_no { get; set; } = string.Empty;
        public string account_type { get; set; } = string.Empty;
        public string account_holder_name { get; set; } = string.Empty;
        public double a_i_o_f { get; set; } = 0.0;                          //Average Income Of Account
        public bool family_saving_account { get; set; } = false;
        public Guid? fk_bank { get; set; } = Guid.NewGuid();
        public tbl_bank? tbl_bank { get; set; } = default!;
        public Guid? fk_citizen { get; set; } = Guid.NewGuid();
        public tbl_citizen? tbl_citizen { get; set; } = default!;
    }
}