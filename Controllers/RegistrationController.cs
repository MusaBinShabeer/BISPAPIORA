using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using BISPAPIORA.Repositories.CitizenServicesRepo;
using BISPAPIORA.Repositories.RegistrationServicesRepo;
using BISPAPIORA.Models.DTOS.RegistrationDTO;
using BISPAPIORA.Extensions.Middleware;

namespace BISPAPIORA.Controllers
{
    // Custom authorization attribute for user authentication
    //[UserAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService registrationService;
        private readonly ICitizenService citizenService;

        // Constructor for Dependency Injection
        public RegistrationController(IRegistrationService registrationService, ICitizenService citizenService)
        {
            this.registrationService = registrationService;
            this.citizenService = citizenService;
        }

        // Add a new registration for a citizen
        [HttpPost]
        public async Task<ActionResult<ResponseModel<RegistrationResponseDTO>>> Post(AddRegistrationDTO model)
        {
            // Check if the model is valid before processing
            if (ModelState.IsValid)
            {
                // Add a new registration and return the response
                var response = registrationService.AddRegisteredCitizen(model);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the model is not valid
                var response = new ResponseModel<RegistrationResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // Update the information of a registered citizen
        [HttpPut]
        public async Task<ActionResult<ResponseModel<RegistrationResponseDTO>>> Put(UpdateRegistrationDTO model)
        {
            // Check if the model is valid before processing
            if (ModelState.IsValid)
            {
                // Update the information of a registered citizen and return the response
                var response = citizenService.UpdateRegisteredCitizen(model);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the model is not valid
                var response = new ResponseModel<RegistrationResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // Delete the registration of a citizen by ID
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<RegistrationResponseDTO>>> Delete(string id)
        {
            // Check if the model state is valid before processing
            if (ModelState.IsValid)
            {
                // Delete the registration and return the response
                var response = registrationService.DeleteRegistration(id);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the model state is not valid
                var response = new ResponseModel<RegistrationResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // Get the registration information of a citizen by ID
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<RegistrationResponseDTO>>> GetById(string id)
        {
            // Check if the ID parameter is not null or empty
            if (!string.IsNullOrEmpty(id))
            {
                // Get the registration information by ID and return the response
                var response = registrationService.GetRegistration(id);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the ID parameter is missing
                var response = new ResponseModel<RegistrationResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // Get the registration information of a citizen by CNIC
        [HttpGet("GetByCnic")]
        public async Task<ActionResult<ResponseModel<RegistrationResponseDTO>>> GetByCnic(string cnic)
        {
            // Check if the CNIC parameter is not null or empty
            if (!string.IsNullOrEmpty(cnic))
            {
                // Get the registration information by CNIC and return the response
                var response = citizenService.GetRegisteredCitizenByCnic(cnic);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the CNIC parameter is missing
                var response = new ResponseModel<RegistrationResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // Verify the registration of a citizen by CNIC
        [HttpGet("VerifyRegistrationWithCNIC")]
        public async Task<ActionResult<ResponseModel<RegistrationResponseDTO>>> VerifyRegistrationWithCNIC(string cnic)
        {
            // Check if the CNIC parameter is not null or empty
            if (!string.IsNullOrEmpty(cnic))
            {
                // Verify the citizen registration with CNIC and return the response
                var response = citizenService.VerifyCitizenRegistrationWithCNIC(cnic);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the CNIC parameter is missing
                var response = new ResponseModel<RegistrationResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // Get the list of all registered citizens
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<RegistrationResponseDTO>>>> Get()
        {
            // Get the list of registered citizens and return the response
            var response = citizenService.GetRegisteredCitizensList();
            return Ok(await response);
        }
    }

}
