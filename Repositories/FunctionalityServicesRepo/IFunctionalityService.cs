using BISPAPIORA.Models.DTOS.FunctionalityDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.FunctionalityServicesRepo
{
    public interface IFunctionalityService
    {
        public Task<ResponseModel<FunctionalityResponseDTO>> AddFunctionality(AddFunctionalityDTO model);
        public Task<ResponseModel<FunctionalityResponseDTO>> DeleteFunctionality(string functionalityId);
        public Task<ResponseModel<List<FunctionalityResponseDTO>>> GetFunctionalitysList();
        public Task<ResponseModel<FunctionalityResponseDTO>> GetFunctionality(string functionalityId);
        public Task<ResponseModel<FunctionalityResponseDTO>> UpdateFunctionality(UpdateFunctionalityDTO model);
    }
}
