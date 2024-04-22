using BISPAPIORA.Models.DTOS.AppVersionDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.AppVersionServiceRepo
{
    public interface IAppVersionService
    {
        public Task<ResponseModel<AppVersionResponseDTO>> AddAppVersion(AddAppVersionDTO model);
        public Task<ResponseModel<AppVersionResponseDTO>> DeleteAppVersion(string AppVersionId);
        public Task<ResponseModel<List<AppVersionResponseDTO>>> GetAppVersionsList();
        public Task<ResponseModel<AppVersionResponseDTO>> GetAppVersion(string AppVersionId);
        public Task<ResponseModel<AppVersionResponseDTO>> UpdateAppVersion(UpdateAppVersionDTO model);
    }
}
