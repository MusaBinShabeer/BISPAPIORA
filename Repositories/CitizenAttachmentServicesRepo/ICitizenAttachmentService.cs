using BISPAPIORA.Models.DTOS.CitizenAttachmentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.CitizenAttachmentServicesRepo
{
    public interface ICitizenAttachmentService
    {
        public Task<ResponseModel<CitizenAttachmentResponseDTO>> AddCitizenAttachment(AddCitizenAttachmentDTO model);
        public Task<ResponseModel<CitizenAttachmentResponseDTO>> DeleteCitizenAttachment(string bankId);
        public Task<ResponseModel<List<CitizenAttachmentResponseDTO>>> GetCitizenAttachmentsList();
        public Task<ResponseModel<CitizenAttachmentResponseDTO>> GetCitizenAttachment(string bankId);
        public Task<ResponseModel<CitizenAttachmentResponseDTO>> UpdateCitizenAttachment(UpdateCitizenAttachmentDTO model);
        public Task<ResponseModel<List<CitizenAttachmentResponseDTO>>> GetCitizenAttachmentByCitizenId(string citizenId);
    }
}
