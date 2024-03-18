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
        public async Task<ActionResult<ResponseModel<ProvinceStatusResponseDTO>>> GetUserPerformanceStats(string id)
        {
            var response = dashboardServices.GetUserPerformanceStats(id);
            return Ok(await response);
        }
    }
}
