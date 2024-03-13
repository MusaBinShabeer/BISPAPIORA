using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.EmploymentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.EmploymentServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class EmploymentController : ControllerBase
    {
        private readonly IEmploymentService employmentService;
        public EmploymentController(IEmploymentService employmentService)
        {
            this.employmentService = employmentService;
        }
        //Add Employment Method
        [HttpPost]
        public async Task<ActionResult<ResponseModel<EmploymentResponseDTO>>> Post(AddEmploymentDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = employmentService.AddEmployment(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EmploymentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Update Employment Method
        [HttpPut]
        public async Task<ActionResult<ResponseModel<EmploymentResponseDTO>>> Put(UpdateEmploymentDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = employmentService.UpdateEmployment(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EmploymentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Delete Employment Method
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<EmploymentResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = employmentService.DeleteEmployment(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EmploymentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Get Employment By Id Method
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<EmploymentResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = employmentService.GetEmployment(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EmploymentResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Get All Employments Method
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<EmploymentResponseDTO>>>> Get()
        {
            var response = employmentService.GetEmploymentsList();
            return Ok(await response);
        }
    }
}
