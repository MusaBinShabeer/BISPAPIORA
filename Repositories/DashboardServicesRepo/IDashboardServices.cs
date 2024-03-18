using BISPAPIORA.Models.DTOS.TehsilStatusResponseDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.DashboardServicesRepo
{
    public interface IDashboardServices
    {
        public Task<ResponseModel<List<TehsilStatusResponseDTO>>> GetTehsilStatusResponses();
    }
}
