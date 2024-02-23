using BISPAPIORA.Models.DTOS.UserDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.UserServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<UserResponseDTO>>> Post(AddUserDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = userService.AddUser(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<UserResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<UserResponseDTO>>> Put(UpdateUserDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = userService.UpdateUser(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<UserResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<UserResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = userService.DeleteUser(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<UserResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<UserResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = userService.GetUser(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<UserResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<UserResponseDTO>>>> Get()
        {
            var response = userService.GetUsersList();
            return Ok(await response);
        }
    }
}
