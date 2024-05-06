using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.DashboardDTO;
using AutoMapper;
using LinqKit;

namespace BISPAPIORA.Repositories.DashboardServicesRepo
{
    public interface IDashboardServices
    {
        public Task<ResponseModel<DashboardUserPerformanceResponseDTO>> GetUserPerformanceStatsForApp(string userEmail, string dateStart, string dateEnd);
        public Task<ResponseModel<WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats>> GetWebDashboardStats();
        public Task<ResponseModel<List<DashboardProvinceCitizenCountPercentageDTO>, List<DashboardDistrictCitizenCountPercentageDTO>, List<DashboardTehsilCitizenCountPercentageDTO>, List<DashboardCitizenEducationalPercentageStatDTO>, List<DashboardCitizenGenderPercentageDTO>, List<DashboardCitizenMaritalStatusPercentageDTO>, List<DashboardCitizenEmploymentPercentageStatDTO>, List<DashboardCitizenCountSavingAmountDTO>, List<DashboardCitizenTrendDTO>, List<WebDashboardStats>>> GetWebDashboardGraphs(string dateStart, string dateEnd, string provinceId, string districtId, string tehsilId, bool registration, bool enrollment, bool compliant);
        public Task<ResponseModel<DashboardDTO>> GetTotalCitizenAndEnrolledForApp();
        public Task<ResponseModel<DashboardCitizenComplianceStatus<List<DashboardQuarterlyStats>>>> GetQuarterlyStatsByCnic(string citizenCnic);
    }
}
