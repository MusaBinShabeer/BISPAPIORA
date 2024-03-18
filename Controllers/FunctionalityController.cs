using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.FunctionalityDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.FunctionalityServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    //[AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionalityController : ControllerBase
    {
        private readonly IFunctionalityService functionalityService;
        public FunctionalityController(IFunctionalityService functionalityService)
        {
            this.functionalityService = functionalityService;
        }
        // Add New Functionality Method
        [HttpPost]
        public async Task<ActionResult<ResponseModel<FunctionalityResponseDTO>>> Post(AddFunctionalityDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = functionalityService.AddFunctionality(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<FunctionalityResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Update Functionality Method
        [HttpPut]
        public async Task<ActionResult<ResponseModel<FunctionalityResponseDTO>>> Put(UpdateFunctionalityDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = functionalityService.UpdateFunctionality(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<FunctionalityResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Delete Functionality Method
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<FunctionalityResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = functionalityService.DeleteFunctionality(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<FunctionalityResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //To get Functionality By id Method
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<FunctionalityResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = functionalityService.GetFunctionality(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<FunctionalityResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //To get all Functionalitys Method
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<FunctionalityResponseDTO>>>> Get()
        {
            var response = functionalityService.GetFunctionalitysList();
            return Ok(await response);
        }
    }
}
