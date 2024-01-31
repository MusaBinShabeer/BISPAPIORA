using BISPAPIORA.Models.DTOS.FileManagerDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.FileManagerServicesRepo
{
    public interface IFileManagerService
    {
        public Task<ResponseModel<FileManagerResponseDTO>> UploadFileAsync(byte[] file,string fileName, string extension);
    }
}
