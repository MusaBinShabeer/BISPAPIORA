namespace BISPAPIORA.Models.DTOS.ResponseDTO
{
    public class ResponseModel<T>
    {
        public T? data { get; set; }
        public bool success { get; set; }
        public string remarks { get; set; } = string.Empty;
    }
    public class ResponseModel
    {
        public bool success { get; set; }
        public string remarks { get; set; } = string.Empty;
    }
}
