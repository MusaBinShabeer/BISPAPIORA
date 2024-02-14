namespace BISPAPIORA.Models.DTOS.FileManagerDTO
{
    public class FileManagerDTOs
    {
        public string filePath { get; set; }= string.Empty;
        public string fileName { get; set; } = string.Empty;
    }
    public class FileManagerResponseDTO: FileManagerDTOs { }
    public class FileManagerImageDTO
    {
        public string fileName { get; set; }= string.Empty;
        public byte[] fileData { get; set; } = new byte[0];
        public string fileContentType { get; set; } = string.Empty;
    }
}
