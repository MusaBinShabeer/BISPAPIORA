using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.CitizenDTO;
using BISPAPIORA.Models.DTOS.RegistrationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.CitizenServicesRepo;
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
        public DashboardController(ICitizenService citizenService)
        {
            this.citizenService = citizenService;
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
    }
}
