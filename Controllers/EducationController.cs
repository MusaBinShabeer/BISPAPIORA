using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.EducationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.EducationServicesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [UserAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducationService educationService;
        public EducationController(IEducationService educationService)
        {
            this.educationService = educationService;
        }
        //Add Education Method
        [HttpPost]
        public async Task<ActionResult<ResponseModel<EducationResponseDTO>>> Post(AddEducationDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = educationService.AddEducation(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EducationResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Update Education Method
        [HttpPut]
        public async Task<ActionResult<ResponseModel<EducationResponseDTO>>> Put(UpdateEducationDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = educationService.UpdateEducation(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EducationResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Delete Education Method
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<EducationResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = educationService.DeleteEducation(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EducationResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Get Education By Id Method
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<EducationResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = educationService.GetEducation(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EducationResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Get All Education Methods
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<EducationResponseDTO>>>> Get()
        {
            var response = educationService.GetEducationsList();
            return Ok(await response);
        }
    }
}
