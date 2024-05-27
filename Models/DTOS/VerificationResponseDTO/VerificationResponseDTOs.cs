namespace BISPAPIORA.Models.DTOS.VerificationResponseDTO
{
    public class ResponseDTO
    {
        public SurvayResponseDTO surveyDetails { get; set; }
        public string remarks { get; set; } = string.Empty;
        public bool success { get; set; }
    }
    public class SurvayResponseDTO
    {
        public string cnic { get; set; }
        public decimal? PMT { get; set; }
        public long? unique_hh_id { get; set; }
        public DateTime? submission_date { get; set; }
    }
}
