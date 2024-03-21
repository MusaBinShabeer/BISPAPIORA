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
    //[AppVersion]
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
        [HttpGet("GetByCnic")]
        public async Task<ActionResult<ResponseModel<CitizenResponseDTO>>> GetByCnic(string cnic)
        {
            // Check if the CNIC parameter is not null or empty
            if (!string.IsNullOrEmpty(cnic))
            {
                // Get the registration information by CNIC and return the response
                var response = citizenService.GetCitizenByCnic(cnic);
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

        [HttpGet("GetTehsilStatus")]
        public async Task<ActionResult<ResponseModel<List<TehsilStatusResponseDTO>>>> GetTehsilStatus()
        {
            var response = dashboardServices.GetTehsilStatusResponses();
            return Ok(await response);
        }

        [HttpGet("GetDistrictStatus")]
        public async Task<ActionResult<ResponseModel<List<DistrictStatusResponseDTO>>>> GetDistrictStatus()
        {
            var response = dashboardServices.GetDistrictStatusResponses();
            return Ok(await response);
        }

        [HttpGet("GetProvinceStatus")]
        public async Task<ActionResult<ResponseModel<List<ProvinceStatusResponseDTO>>>> GetProvinceStatus()
        {
            var response = dashboardServices.GetProvinceStatusResponses();
            return Ok(await response);
        }

        [HttpGet("GetUserPerformanceStats")]
        public async Task<ActionResult<ResponseModel<DashboardUserPerformanceResponseDTO>>> GetUserPerformanceStats(string usernName, string dateStart = null, string dateEnd = null)
        {
            if ((string.IsNullOrEmpty(dateStart) && string.IsNullOrEmpty(dateEnd)) || (dateStart != null && dateEnd != null))
            {
                var response = dashboardServices.GetUserPerformanceStats(usernName, dateStart, dateEnd);
                return Ok(await response);
            }
            else
            {
                return BadRequest("Both dateStart and dateEnd must be provided.");
            }

        }

        [HttpGet("GetWebDesktopApplicantDistribution")]
        public async Task<ActionResult<ResponseModel<DashboardUserPerformanceResponseDTO>>> GetWebDesktopApplicantDistribution(string usernName, string dateStart = null, string dateEnd = null)
        {
            if ((string.IsNullOrEmpty(dateStart) && string.IsNullOrEmpty(dateEnd)) || (dateStart != null && dateEnd != null))
            {
                var response = dashboardServices.GetWebDesktopApplicantDistribution(usernName, dateStart, dateEnd);
                return Ok(await response);
            }
            else
            {
                return BadRequest("Both dateStart and dateEnd must be provided.");
            }

        }

        [HttpGet("GetWebDesktopApplicantDistributionLocationBased")]
        public async Task<ActionResult<ResponseModel<DashboardUserPerformanceResponseDTO>>> GetWebDesktopApplicantDistributionLocationBased(string usernName, string dateStart = null, string dateEnd = null, string provinceName = null, string districtName = null, string tehsilName = null)
        {
            if ((string.IsNullOrEmpty(dateStart) && string.IsNullOrEmpty(dateEnd)) || (dateStart != null && dateEnd != null))
            {
                var response = dashboardServices.GetWebDesktopApplicantDistributionLocationBased(usernName, dateStart, dateEnd, provinceName, districtName, tehsilName);
                return Ok(await response);
            }
            else
            {
                return BadRequest("Both dateStart and dateEnd must be provided.");
            }

        }

        [HttpGet("GetTotalCitizens")]
        public async Task<ActionResult<ResponseModel<List<ProvinceStatusResponseDTO>>>> GetTotalCitizens()
        {
            var response = dashboardServices.GetTotalCitizenAndEnrolled();
            return Ok(await response);
        }

        //[HttpGet("GetTotalCompliantApplicants")]
        //public async Task<ActionResult<ResponseModel<List<ProvinceStatusResponseDTO>>>> GetTotalCompliantApplicants()
        //{
        //    var response = dashboardServices.GetTotalCompliantApplicants();
        //    return Ok(await response);
        //}

        //[HttpGet("GetTotalSavings")]
        //public async Task<ActionResult<ResponseModel<List<ProvinceStatusResponseDTO>>>> GetTotalSavings()
        //{
        //    var response = dashboardServices.GetTotalSavings();
        //    return Ok(await response);
        //}

        //[HttpGet("GetTotalMatchingGrants")]
        //public async Task<ActionResult<ResponseModel<List<ProvinceStatusResponseDTO>>>> GetTotalMatchingGrants()
        //{
        //    var response = dashboardServices.GetTotalCitizenAndEnrolled();
        //    return Ok(await response);
        //}
    }
}
