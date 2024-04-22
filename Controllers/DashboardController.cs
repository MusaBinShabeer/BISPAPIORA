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


        // Retrieves citizen information for the given CNIC for the application.
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

        /// <summary>
        /// Retrieves citizens' data for web application based on specified filters.
        /// </summary>
        /// <param name="dateStart">The start date for filtering citizens' data.</param>
        /// <param name="dateEnd">The end date for filtering citizens' data.</param>
        /// <param name="provinceId">The province ID for filtering citizens' data.</param>
        /// <param name="districtId">The district ID for filtering citizens' data.</param>
        /// <param name="tehsilId">The tehsil ID for filtering citizens' data.</param>
        /// <param name="registration">A boolean indicating whether to include registered citizens.</param>
        /// <param name="enrollment">A boolean indicating whether to include enrolled citizens.</param>
        /// <returns>An HTTP response containing the citizens' data for the web application.</returns>
        [HttpGet("GetCitizensForWeb")]
        public async Task<ActionResult<ResponseModel<List<CitizenResponseDTO>>>> GetCitizensForWeb(string dateStart = null, string dateEnd = null, string provinceId = null, string districtId = null, string tehsilId = null, bool registration = false, bool enrollment = false)
        {
            // Check if the CNIC parameter is not null or empty

            // Get the registration information by CNIC and return the response
            var response = citizenService.GetCitizensDataForWeb(dateStart, dateEnd, provinceId, districtId, tehsilId, registration, enrollment);
            return Ok(await response);

        }

        /// <summary>
        /// Retrieves the performance statistics for a user within the specified date range.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="dateStart">The start date for filtering performance statistics.</param>
        /// <param name="dateEnd">The end date for filtering performance statistics.</param>
        /// <returns>
        ///     An HTTP response containing the performance statistics for the user or a bad request response if the date range is not valid.
        /// </returns>
        [HttpGet("GetUserPerformanceStatsForApp")]
        public async Task<ActionResult<ResponseModel<DashboardUserPerformanceResponseDTO>>> GetUserPerformanceStatsForApp(string userName, string dateStart = null, string dateEnd = null)
        {
            // Check if both dateStart and dateEnd are provided or both are null
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
        
        // Retrieves web dashboard statistics for the application.
        [HttpGet("GetWebDashboardStats")]
        public async Task<ActionResult<ResponseModel<WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats>>> GetWebDashboardStats()
        {
            var response = dashboardServices.GetWebDashboardStats();
            return Ok(await response);
        }

        /// <summary>
        /// Retrieves filtered web dashboard graphs based on specified filters.
        /// </summary>
        /// <param name="dateStart">The start date for filtering the dashboard graphs.</param>
        /// <param name="dateEnd">The end date for filtering the dashboard graphs.</param>
        /// <param name="provinceId">The province ID for filtering the dashboard graphs.</param>
        /// <param name="districtId">The district ID for filtering the dashboard graphs.</param>
        /// <param name="tehsilId">The tehsil ID for filtering the dashboard graphs.</param>
        /// <param name="registration">A boolean indicating whether to include registered citizens.</param>
        /// <param name="enrollment">A boolean indicating whether to include enrolled citizens.</param>
        /// <returns>
        ///     An HTTP response containing filtered web dashboard graphs.
        /// </returns>
        [HttpGet("GetWebDashboardGraphsFiltered")]
        public async Task<ActionResult<ResponseModel<List<DashboardProvinceCitizenCountPercentageDTO>, List<DashboardDistrictCitizenCountPercentageDTO>, List<DashboardTehsilCitizenCountPercentageDTO>, List<DashboardCitizenEducationalPercentageStatDTO>, List<DashboardCitizenGenderPercentageDTO>, List<DashboardCitizenMaritalStatusPercentageDTO>, List<DashboardCitizenEmploymentPercentageStatDTO>, List<DashboardCitizenCountSavingAmountDTO>, List<DashboardCitizenTrendDTO>, List<WebDashboardStats>>>> GetWebDashboardGraphsFiltered(
            string dateStart = null,
            string dateEnd = null,
            string provinceId = null,
            string districtId = null,
            string tehsilId = null,
            bool registration = false,
            bool enrollment = false)
        {
            var response = dashboardServices.GetWebDesktopApplicantDistributionLocationBased(dateStart, dateEnd, provinceId, districtId, tehsilId, registration, enrollment);
            return Ok(await response);
        }

        // Retrieves total citizen count and enrolled count for thmoe application.
        [HttpGet("GetTotalCitizenAndEnrolledForApp")]
        public async Task<ActionResult<ResponseModel<List<DashboardDTO>>>> GetTotalCitizenAndEnrolledForApp()
        {
            var response = dashboardServices.GetTotalCitizenAndEnrolledForApp();
            return Ok(await response);
        }
        [HttpGet("GetComplianceStatsByCnic")]
        public async Task<ActionResult<DashboardCitizenComplianceStatus<List<DashboardQuarterlyStats>>>> GetComplianceStatsByCnic(string citizenCnic)
        {
            if (!string.IsNullOrEmpty(citizenCnic))
            {
                var response = dashboardServices.GetQuarterlyStatsByCnic(citizenCnic);
                return Ok(await response);
            }
            else 
            {
                var response = new ResponseModel<CitizenResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
          
        }

    }
}
