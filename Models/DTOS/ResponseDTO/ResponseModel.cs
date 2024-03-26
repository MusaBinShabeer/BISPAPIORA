namespace BISPAPIORA.Models.DTOS.ResponseDTO
{
    public class ResponseModel<T>
    {
        public T? data { get; set; }
        public bool success { get; set; }
        public string remarks { get; set; } = string.Empty;
    }
    public class ResponseModel<T1, T2, T3,T4, T5, T6, T7>
    public class ResponseModel<T1,T2,T3, T4, T5, T6, T7, T8, T9>
    {
        public T1? ProvinceWise { get; set; }
        public T2? DistrictWise { get; set; }
        public T3? TehsilWise { get; set; }
        public T5? GenderWise { get; set; }
        public T6? MaritalStatusWise { get; set; }
        public T7? EmployementWise { get; set; }
        public T4? educationalWise { get; set; }
        public T4? GenderWise { get; set; }
        public T5? MaritalStatusWise { get; set; }
        public T6? EducationalWise { get; set; }
        public T7? EmployementWise { get; set; }
        public T8? SavingAmountWise { get; set; }
        public T9? CitizenTrendWise { get; set; }
        public bool success { get; set; }
        public string remarks { get; set; } = string.Empty;
    }
    public class ResponseModel
    {
        public bool success { get; set; }
        public string remarks { get; set; } = string.Empty;
    }
}
