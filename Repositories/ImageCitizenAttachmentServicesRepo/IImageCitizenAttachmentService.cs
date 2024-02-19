using BISPAPIORA.Models.DTOS.CitizenAttachmentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ImageCitizenAttachmentDTO;

namespace BISPAPIORA.Repositories.ImageCitizenAttachmentServicesRepo
{
    public interface IImageCitizenAttachmentService
    {
        public Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> AddImageCitizenAttachment(AddImageCitizenAttachmentDTO model);
        public Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> DeleteImageCitizenAttachment(string bankId);
        public Task<ResponseModel<List<ImageCitizenAttachmentResponseDTO>>> GetImageCitizenAttachmentsList();
        public Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> GetImageCitizenAttachment(string bankId);
        public Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> UpdateImageCitizenAttachment(UpdateImageCitizenAttachmentDTO model);
        public Task<ResponseModel<List<ImageCitizenAttachmentResponseDTO>>> GetImageCitizenAttachmentByCitizenId(string citizenId);
        public Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> GetImageCitizenAttachmentByCitizenCnic(string citizenCnic);
    }
}
