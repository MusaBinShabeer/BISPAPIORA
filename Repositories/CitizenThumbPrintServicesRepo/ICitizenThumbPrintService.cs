using BISPAPIORA.Models.DTOS.CitizenThumbPrintDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.CitizenThumbPrintServicesRepo
{
    public interface ICitizenThumbPrintService
    {
        public Task<ResponseModel<CitizenThumbPrintResponseDTO>> AddCitizenThumbPrint(AddCitizenThumbPrintDTO model);
        public Task<ResponseModel<CitizenThumbPrintResponseDTO>> DeleteCitizenThumbPrint(string bankId);
        public Task<ResponseModel<List<CitizenThumbPrintResponseDTO>>> GetCitizenThumbPrintsList();
        public Task<ResponseModel<CitizenThumbPrintResponseDTO>> GetCitizenThumbPrint(string bankId);
        public Task<ResponseModel<CitizenThumbPrintResponseDTO>> UpdateCitizenThumbPrint(UpdateCitizenThumbPrintDTO model);
        public Task<ResponseModel<List<CitizenThumbPrintResponseDTO>>> GetCitizenThumbPrintByCitizenId(string citizenId);
    }
}
