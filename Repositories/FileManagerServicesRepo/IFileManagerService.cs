using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.FileManagerServicesRepo
{
    public interface IFileManagerService
    {
        public Task<ResponseModel> UploadFileAsync(byte[] file,string fileName, string extension);
    }
}
