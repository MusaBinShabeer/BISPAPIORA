using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.CitizenSchemeDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.CitizenSchemeServicesRepo
{
    public interface ICitizenSchemeService
    {
        public Task<ResponseModel<CitizenSchemeResponseDTO>> AddCitizenScheme(AddCitizenSchemeDTO model);
        public Task<ResponseModel<CitizenSchemeResponseDTO>> DeleteCitizenScheme(string bankId);
        public Task<ResponseModel<List<CitizenSchemeResponseDTO>>> GetCitizenSchemesList();
        public Task<ResponseModel<CitizenSchemeResponseDTO>> GetCitizenScheme(string bankId);
        public Task<ResponseModel<CitizenSchemeResponseDTO>> UpdateCitizenScheme(UpdateCitizenSchemeDTO model);
    }
}
