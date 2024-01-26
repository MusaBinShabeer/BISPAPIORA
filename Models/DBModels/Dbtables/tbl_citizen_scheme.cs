using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DBModels.Dbtables
{
    public class tbl_citizen_scheme
    {
        [Key]
        public Guid citizen_scheme_id { get; set; } = Guid.NewGuid();
        public string citizen_scheme_year { get; set; } = string.Empty;
        public string citizen_scheme_quarter { get; set; } = string.Empty;
        public string citizen_scheme_starting_month { get; set; } = string.Empty;
        public double citizen_scheme_saving_amount { get; set; } = 0.0;
        public Guid? fk_citizen { get; set; } = Guid.NewGuid();
        public tbl_citizen? tbl_citizen { get; set; } = default!;
    }
}