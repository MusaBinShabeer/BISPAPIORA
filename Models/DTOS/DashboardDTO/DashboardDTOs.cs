namespace BISPAPIORA.Models.DTOS.DashboardDTO
{
    public class DashboardUserPerformanceResponseDTO
    {
        public int registeredCount { get; set; } = 0;
        public int enrolledCount { get; set; } = 0;
    }
    public class DashboardCitizenBaseModel
    {
        public Guid citizen_id { get; set; }
        public string citizen_name { get; set; } =string.Empty;
        public Guid registered_by { get; set; }= default!;
        public DateTime registered_date{ get; set; }= default!;
        public Guid enrolled_by { get;set; }= default!;
        public DateTime enrolled_date { get;set; }= default!;
    }
}
