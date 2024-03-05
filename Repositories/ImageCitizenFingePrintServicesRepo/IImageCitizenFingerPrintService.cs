//using BISPAPIORA.Models.DTOS.CitizenAttachmentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ImageCitizenAttachmentDTO;
using BISPAPIORA.Models.DTOS.ImageCitizenFingerPrintDTO;

namespace BISPAPIORA.Repositories.ImageCitizenFingePrintServicesRepo
{
    public interface IImageCitizenFingerPrintService
    {
        public Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> AddImageCitizenFingerPrint(AddImageCitizenFingerPrintDTO model);

        public Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> AddFkCitizentoImage(AddImageCitizenFingerPrintDTO model);
        public Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> DeleteImageCitizenFingerPrint(string bankId);
        public Task<ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>> GetImageCitizenFingerPrintsList();
        public Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> GetImageCitizenFingerPrint(string bankId);
        public Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> UpdateImageCitizenFingerPrint(UpdateImageCitizenFingerPrintDTO model);
        public Task<ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>> GetImageCitizenFingerPrintByCitizenId(string citizenId);
        public Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> GetImageCitizenFingerPrintByCitizenCnic(string citizenCnic);
    }
}
