using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using BISPAPIORA.Repositories.CitizenServicesRepo;
using BISPAPIORA.Repositories.RegistrationServicesRepo;
using BISPAPIORA.Models.DTOS.RegistrationDTO;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService registrationService;
        private readonly ICitizenService citizenService;
        public RegistrationController(IRegistrationService registrationService, ICitizenService citizenService)
        {
            this.registrationService = registrationService;
            this.citizenService = citizenService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<RegistrationResponseDTO>>> Post(AddRegistrationDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = registrationService.AddRegisteredCitizen(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<RegistrationResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<RegistrationResponseDTO>>> Put(UpdateRegistrationDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = citizenService.UpdateRegisteredCitizen(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<RegistrationResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<RegistrationResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = registrationService.DeleteRegistration(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<RegistrationResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<RegistrationResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = registrationService.GetRegistration(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<RegistrationResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }

        [HttpGet("GetByCnic")]
        public async Task<ActionResult<ResponseModel<RegistrationResponseDTO>>> GetByCnic(string cnic)
        {
            if (!string.IsNullOrEmpty(cnic))
            {
                var response = citizenService.GetRegisteredCitizenByCnic(cnic);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<RegistrationResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<RegistrationResponseDTO>>>> Get()
        {
            var response = citizenService.GetRegisteredCitizensList();
            return Ok(await response);
        }
    }
}
