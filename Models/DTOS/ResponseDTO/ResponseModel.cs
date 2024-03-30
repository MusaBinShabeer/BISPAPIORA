namespace BISPAPIORA.Models.DTOS.ResponseDTO
{
    public class ResponseModel<T>
    {
        public T? data { get; set; }
        public bool success { get; set; }
        public string remarks { get; set; } = string.Empty;
    }
    
    public class ResponseModel<T1,T2,T3, T4, T5, T6, T7, T8, T9, T10>
    {
        public T1? provinceWise { get; set; }
        public T2? districtWise { get; set; }
        public T3? tehsilWise { get; set; }
        public T4? educationalWise { get; set; }
        public T5? genderWise { get; set; }
        public T6? maritalStatusWise { get; set; }
        public T7? employementWise { get; set; }
        public T8? savingAmountWise { get; set; }
        public T9? citizenTrendWise { get; set; }
        public T10? citizenCountWise { get; set; }
        public bool success { get; set; }
        public string remarks { get; set; } = string.Empty;
    }
    public class ResponseModel<T1, T2, T3, T4, T5>
    {
        public T1? totalApplicants { get; set; }
        public T2? totalEnrollments { get; set; }
        public T3? totalSavings { get; set; }
        public T4? complaintApplicants { get; set; }
        public T5? matchingGrants { get; set; }      
        public bool success { get; set; }
        public string remarks { get; set; } = string.Empty;
    }
    public class ResponseModel
    {
        public bool success { get; set; }
        public string remarks { get; set; } = string.Empty;
    }
}
