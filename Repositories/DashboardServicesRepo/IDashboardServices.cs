using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.DashboardDTO;
using AutoMapper;
using LinqKit;

namespace BISPAPIORA.Repositories.DashboardServicesRepo
{
    public interface IDashboardServices
    {
        public  Task<ResponseModel<DashboardUserPerformanceResponseDTO>> GetUserPerformanceStatsForApp(string userName, string dateStart, string dateEnd);
        public  Task<ResponseModel<WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats>> GetWebDashboardStats();
        public Task<ResponseModel<List<DashboardProvinceCitizenCountPercentageDTO>, List<DashboardDistrictCitizenCountPercentageDTO>, List<DashboardTehsilCitizenCountPercentageDTO>, List<DashboardCitizenEducationalPercentageStatDTO>, List<DashboardCitizenGenderPercentageDTO>, List<DashboardCitizenMaritalStatusPercentageDTO>, List<DashboardCitizenEmploymentPercentageStatDTO>, List<DashboardCitizenCountSavingAmountDTO>, List<DashboardCitizenTrendDTO>, List<WebDashboardStats>>> GetWebDesktopApplicantDistributionLocationBased(string dateStart, string dateEnd, string provinceId, string districtId, string tehsilId, bool registration, bool enrollment);
        public  Task<ResponseModel<DashboardDTO>> GetTotalCitizenAndEnrolledForApp();
    }
}
