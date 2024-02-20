using BISPAPIORA.Models.DTOS.CitizenAttachmentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ImageCitizenAttachmentDTO;
using BISPAPIORA.Models.DTOS.ImageCitizenThumbPrintDTO;

namespace BISPAPIORA.Repositories.ImageCitizenThumbPrintServicesRepo
{
    public interface IImageCitizenThumbPrintService
    {
        public Task<ResponseModel<ImageCitizenThumbPrintResponseDTO>> AddImageCitizenThumbPrint(AddImageCitizenThumbPrintDTO model);

        public Task<ResponseModel<ImageCitizenThumbPrintResponseDTO>> AddFkCitizentoImage(AddImageCitizenThumbPrintDTO model);
        public Task<ResponseModel<ImageCitizenThumbPrintResponseDTO>> DeleteImageCitizenThumbPrint(string bankId);
        public Task<ResponseModel<List<ImageCitizenThumbPrintResponseDTO>>> GetImageCitizenThumbPrintsList();
        public Task<ResponseModel<ImageCitizenThumbPrintResponseDTO>> GetImageCitizenThumbPrint(string bankId);
        public Task<ResponseModel<ImageCitizenThumbPrintResponseDTO>> UpdateImageCitizenThumbPrint(UpdateImageCitizenThumbPrintDTO model);
        public Task<ResponseModel<List<ImageCitizenThumbPrintResponseDTO>>> GetImageCitizenThumbPrintByCitizenId(string citizenId);
        public Task<ResponseModel<ImageCitizenThumbPrintResponseDTO>> GetImageCitizenThumbPrintByCitizenCnic(string citizenCnic);
    }
}
