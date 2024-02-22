using BISPAPIORA.Models.DTOS.TehsilStatusResponseDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.TehsilStatusResponseServicesRepo
{
    public interface ITehsilStatusResponseService
    {
        public Task<ResponseModel<List<TehsilStatusResponseDTO>>> GetTehsilStatusResponses();
    }
}
