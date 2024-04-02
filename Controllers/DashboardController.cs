using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.CitizenDTO;
using BISPAPIORA.Models.DTOS.DashboardDTO;
using BISPAPIORA.Models.DTOS.RegistrationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.CitizenServicesRepo;
using BISPAPIORA.Repositories.DashboardServicesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ICitizenService citizenService;
        private readonly IDashboardServices dashboardServices;
        public DashboardController(ICitizenService citizenService, IDashboardServices dashboardServices)
        {
            this.citizenService = citizenService;
            this.dashboardServices = dashboardServices;
        }
        [HttpGet("GetByCnicForApp")]
        public async Task<ActionResult<ResponseModel<CitizenResponseDTO>>> GetByCnicForApp(string cnic)
        {
            // Check if the CNIC parameter is not null or empty
            if (!string.IsNullOrEmpty(cnic))
            {
                // Get the registration information by CNIC and return the response
                var response = citizenService.GetCitizenByCnicForApp(cnic);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the CNIC parameter is missing
                var response = new ResponseModel<CitizenResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("GetCitizensForWeb")]
        public async Task<ActionResult<ResponseModel<List<CitizenResponseDTO>>>> GetCitizensForWeb(string dateStart = null, string dateEnd = null, string provinceId = null, string districtId = null, string tehsilId = null, bool registration = false, bool enrollment = false)
        {
            // Check if the CNIC parameter is not null or empty
           
                // Get the registration information by CNIC and return the response
                var response = citizenService.GetCitizensDataForWeb(dateStart, dateEnd, provinceId, districtId, tehsilId, registration, enrollment);
                return Ok(await response);
           
        }
        [HttpGet("GetUserPerformanceStatsForApp")]
        public async Task<ActionResult<ResponseModel<DashboardUserPerformanceResponseDTO>>> GetUserPerformanceStatsForApp(string userName, string dateStart = null, string dateEnd = null)
        {
            if ((string.IsNullOrEmpty(dateStart) && string.IsNullOrEmpty(dateEnd)) || (dateStart != null && dateEnd != null))
            {
                var response = dashboardServices.GetUserPerformanceStatsForApp(userName, dateStart, dateEnd);
                return Ok(await response);
            }
            else
            {
                return BadRequest("Both dateStart and dateEnd must be provided.");
            }

        }

        [HttpGet("GetWebDashboardStats")]
        public async Task<ActionResult<ResponseModel<WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats>>> GetWebDashboardStats()
        {
            
                var response = dashboardServices.GetWebDashboardStats();
                return Ok(await response);            
        }

        [HttpGet("GetWebDashboardGraphsFiltered")]
        public async Task<ActionResult<ResponseModel<List<DashboardProvinceCitizenCountPercentageDTO>, List<DashboardDistrictCitizenCountPercentageDTO>, List<DashboardTehsilCitizenCountPercentageDTO>, List<DashboardCitizenEducationalPercentageStatDTO>, List<DashboardCitizenGenderPercentageDTO>, List<DashboardCitizenMaritalStatusPercentageDTO>, List<DashboardCitizenEmploymentPercentageStatDTO>, List<DashboardCitizenCountSavingAmountDTO>, List<DashboardCitizenTrendDTO>, List<DashboardDTO>>>> GetWebDashboardGraphsFiltered( string dateStart = null, string dateEnd = null, string provinceId = null, string districtId = null, string tehsilId = null, bool registration = false, bool enrollment = false)
        {   
            var response = dashboardServices.GetWebDesktopApplicantDistributionLocationBased(dateStart, dateEnd, provinceId, districtId, tehsilId, registration, enrollment);
            return Ok(await response);
        }

        [HttpGet("GetTotalCitizenAndEnrolledForApp")]
        public async Task<ActionResult<ResponseModel<List<DashboardDTO>>>> GetTotalCitizenAndEnrolledForApp()
        {
            var response = dashboardServices.GetTotalCitizenAndEnrolledForApp();
            return Ok(await response);
        }        
    }
}
