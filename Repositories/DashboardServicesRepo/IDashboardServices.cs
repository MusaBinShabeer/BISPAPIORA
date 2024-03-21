using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.DashboardDTO;
using AutoMapper;
using LinqKit;

namespace BISPAPIORA.Repositories.DashboardServicesRepo
{
    public interface IDashboardServices
    {
        public Task<ResponseModel<DashboardUserPerformanceResponseDTO>> GetUserPerformanceStats(string userIdstring, string dateStart, string dateEnd);
        public Task<ResponseModel<DashboardCitizenCountPercentageDTO>> GetWebDesktopApplicantDistribution(string userName, string dateStart, string dateEnd);

        public Task<ResponseModel<List<DashboardTehsilCitizenCountPercentageDTO>>> GetWebDesktopApplicantDistributionLocationBased(string userName, string dateStart, string dateEnd, string provinceName, string districtName, string tehsilname);
        public Task<ResponseModel<List<TehsilStatusResponseDTO>>> GetTehsilStatusResponses();
        public Task<ResponseModel<List<DistrictStatusResponseDTO>>> GetDistrictStatusResponses();
        public Task<ResponseModel<List<ProvinceStatusResponseDTO>>> GetProvinceStatusResponses();
        public Task<ResponseModel<DashboardDTO>> GetTotalCitizenAndEnrolled();
        //public Task<ResponseModel<DashboardDTO>> GetTotalCompliantApplicants();
        //public Task<ResponseModel<DashboardDTO>> GetTotalSavings();
        //public Task<ResponseModel<DashboardDTO>> GetTotalMatchingGrants();
    }
}
