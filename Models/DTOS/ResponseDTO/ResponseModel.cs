namespace BISPAPIORA.Models.DTOS.ResponseDTO
{
    public class ResponseModel<T>
    {
        public T? data { get; set; }
        public bool success { get; set; }
        public string remarks { get; set; } = string.Empty;
    }
    public class ResponseModel<T1,T2,T3,T4>
    {
        public T1? ProvinceWise { get; set; }
        public T2? DistrictWise { get; set; }
        public T3? TehsilWise { get; set; }
        public T4? educationalWise { get; set; }
        public bool success { get; set; }
        public string remarks { get; set; } = string.Empty;
    }
    public class ResponseModel
    {
        public bool success { get; set; }
        public string remarks { get; set; } = string.Empty;
    }
}
