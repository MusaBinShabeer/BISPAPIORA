using BISPAPIORA.Models.DTOS.ProvinceStatusResponseDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.ProvinceStatusResponseServicesRepo
{
    public interface IProvinceStatusResponseService
    {
        public Task<ResponseModel<List<ProvinceStatusResponseDTO>>> GetProvinceStatusResponses();
    }
}
