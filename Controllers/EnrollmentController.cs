using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.CitizenServicesRepo;
using BISPAPIORA.Repositories.EnrollmentServicesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    // Custom authorization attribute for user authentication
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService enrollmentService;
        private readonly ICitizenService citizenService;

        // Constructor for Dependency Injection
        public EnrollmentController(IEnrollmentService enrollmentService, ICitizenService citizenService)
        {
            this.enrollmentService = enrollmentService;
            this.citizenService = citizenService;
        }

        // Add a new enrollment for a citizen
        [HttpPost]
        public async Task<ActionResult<ResponseModel<EnrollmentResponseDTO>>> Post(AddEnrollmentDTO model)
        {
            // Check if the model is valid before processing
            if (ModelState.IsValid)
            {
                // Add a new enrollment and return the response
                var response = enrollmentService.AddEnrolledCitizen(model);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the model is not valid
                var response = new ResponseModel<EnrollmentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // Update the information of an enrolled citizen
        [HttpPut]
        public async Task<ActionResult<ResponseModel<EnrollmentResponseDTO>>> Put(UpdateEnrollmentDTO model)
        {
            // Check if the model is valid before processing
            if (ModelState.IsValid)
            {
                // Update the information of an enrolled citizen and return the response
                var response = citizenService.UpdateEnrolledCitizen(model);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the model is not valid
                var response = new ResponseModel<EnrollmentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // Delete the enrollment of a citizen by ID
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<EnrollmentResponseDTO>>> Delete(string id)
        {
            // Check if the model state is valid before processing
            if (ModelState.IsValid)
            {
                // Delete the enrollment and return the response
                var response = enrollmentService.DeleteEnrollment(id);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the model state is not valid
                var response = new ResponseModel<EnrollmentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // Get the enrollment information of a citizen by ID
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<EnrollmentResponseDTO>>> GetById(string id)
        {
            // Check if the ID parameter is not null or empty
            if (!string.IsNullOrEmpty(id))
            {
                // Get the enrollment information by ID and return the response
                var response = enrollmentService.GetEnrollment(id);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the ID parameter is missing
                var response = new ResponseModel<EnrollmentResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // Get the enrollment information of a citizen by CNIC
        [HttpGet("GetByCnic")]
        public async Task<ActionResult<ResponseModel<EnrollmentResponseDTO>>> GetByCnic(string cnic)
        {
            // Check if the CNIC parameter is not null or empty
            if (!string.IsNullOrEmpty(cnic))
            {
                // Get the enrollment information by CNIC and return the response
                var response = enrollmentService.GetEnrollment(cnic);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the CNIC parameter is missing
                var response = new ResponseModel<EnrollmentResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("VerifyRegistrationForEnrollment")]
        public async Task<ActionResult<ResponseModel<EnrollmentResponseDTO>>> VerifyRegistrationForEnrollment(string cnic)
        {
            // Check if the CNIC parameter is not null or empty
            if (!string.IsNullOrEmpty(cnic))
            {
                // Get the enrollment information by CNIC and return the response
                var response = citizenService.VerifyCitizenEnrollmentWithCNIC(cnic);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the CNIC parameter is missing
                var response = new ResponseModel<EnrollmentResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("VerifyEnrollmentByCnic")]
        public async Task<ActionResult<ResponseModel<EnrollmentSchemeResponseDTO>>> VerifyEnrollmentByCnic(string cnic)
        {
            // Check if the CNIC parameter is not null or empty
            if (!string.IsNullOrEmpty(cnic))
            {
                // Get the enrollment information by CNIC and return the response
                var response = citizenService.VerifyCitizenEnrollmentStatusWithCNIC(cnic);
                return Ok(await response);
            }
            else
            {
                // Return a bad request response if the CNIC parameter is missing
                var response = new ResponseModel<EnrollmentResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }


        // Get the list of all enrolled citizens
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<EnrollmentResponseDTO>>>> Get()
        {
            // Get the list of enrolled citizens and return the response
            var response = citizenService.GetEnrolledCitizensList();
            return Ok(await response);
        }
    }
}
