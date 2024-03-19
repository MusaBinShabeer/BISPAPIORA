using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.DashboardDTO;

namespace BISPAPIORA.Repositories.DashboardServicesRepo
{
    public interface IDashboardServices
    {
        public Task<ResponseModel<DashboardUserPerformanceResponseDTO>> GetUserPerformanceStats(string userIdstring,string dateStart, string dateEnd);
        public Task<ResponseModel<List<TehsilStatusResponseDTO>>> GetTehsilStatusResponses();
        public Task<ResponseModel<List<DistrictStatusResponseDTO>>> GetDistrictStatusResponses();
        public Task<ResponseModel<List<ProvinceStatusResponseDTO>>> GetProvinceStatusResponses();
    }
}
