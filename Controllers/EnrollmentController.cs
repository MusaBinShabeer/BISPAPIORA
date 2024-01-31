using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.CitizenServicesRepo;
using BISPAPIORA.Repositories.EnrollmentServicesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService enrollmentService;
        private readonly ICitizenService citizenService;
        public EnrollmentController(IEnrollmentService enrollmentService, ICitizenService citizenService)
        {
            this.enrollmentService = enrollmentService;
            this.citizenService = citizenService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<EnrollmentResponseDTO>>> Post(AddEnrollmentDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = enrollmentService.AddEnrolledCitizen(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EnrollmentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<EnrollmentResponseDTO>>> Put(UpdateEnrollmentDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = citizenService.UpdateEnrolledCitizen(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EnrollmentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<EnrollmentResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = enrollmentService.DeleteEnrollment(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EnrollmentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<EnrollmentResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = enrollmentService.GetEnrollment(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EnrollmentResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("GetByCnic")]
        public async Task<ActionResult<ResponseModel<EnrollmentResponseDTO>>> GetByCnic(string cnic)
        {
            if (!string.IsNullOrEmpty(cnic))
            {
                var response = enrollmentService.GetEnrollment(cnic);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EnrollmentResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<EnrollmentResponseDTO>>>> Get()
        {
            var response = citizenService.GetEnrolledCitizensList();
            return Ok(await response);
        }
    }
}
