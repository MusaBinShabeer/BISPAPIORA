using BISPAPIORA.Models.DTOS.UserTypeDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.UserTypeServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BISPAPIORA.Extensions.Middleware;

namespace BISPAPIORA.Controllers
{
    [UserAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService userTypeService;
        public UserTypeController(IUserTypeService userTypeService)
        {
            this.userTypeService = userTypeService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<UserTypeResponseDTO>>> Post(AddUserTypeDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = userTypeService.AddUserType(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<UserTypeResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<UserTypeResponseDTO>>> Put(UpdateUserTypeDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = userTypeService.UpdateUserType(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<UserTypeResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<UserTypeResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = userTypeService.DeleteUserType(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<UserTypeResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<UserTypeResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = userTypeService.GetUserType(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<UserTypeResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<UserTypeResponseDTO>>>> Get()
        {
            var response = userTypeService.GetUserTypesList();
            return Ok(await response);
        }
    }
}
