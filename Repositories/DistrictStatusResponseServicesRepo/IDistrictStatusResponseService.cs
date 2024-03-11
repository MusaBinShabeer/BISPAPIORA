using BISPAPIORA.Models.DTOS.DistrictStatusResponseDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.DistrictStatusResponseServicesRepo
{
    public interface IDistrictStatusResponseService
    {
        public Task<ResponseModel<List<DistrictStatusResponseDTO>>> GetDistrictStatusResponses();
    }
}
