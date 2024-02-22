using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.ReportingResponseDTO
{
    public class ReportingResponseDTO
    {
        public int totalCount { get; set; } = 0;
        public int enrolledCount { get; set; } = 0;
        public int registeredCount { get; set; } = 0;
        //public double percentageEnrolled { get; set; } = 0.0;
        //public double percentageRegistered { get; set; } = 0.0;
    }
}
