using BISPAPIORA.Models.DTOS.UserDTOs;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.UserServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BISPAPIORA.Extensions.Middleware;

namespace BISPAPIORA.Controllers
{
    // Controller for managing user-related operations
    // Requires user authentication
    [UserAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        // Constructor to inject the userService dependency
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        // POST api/User
        // Endpoint for adding a new user
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

        // PUT api/User
        // Endpoint for updating an existing user
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

        // PUT api/User/UpdateUserFTP
        // Endpoint for updating user FTP information
        [HttpPut("UpdateUserFTP")]
        public async Task<ActionResult<ResponseModel<UserResponseDTO>>> UpdateUserFTP(UpdateUserFtpDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = userService.UpdateFTP(model);
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

        // DELETE api/User
        // Endpoint for deleting a user by ID
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

        // GET api/User/GetById
        // Endpoint for getting a user by ID
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

        // GET api/User
        // Endpoint for getting a list of all users
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<UserResponseDTO>>>> Get()
        {
            var response = userService.GetUsersList();
            return Ok(await response);
        }
    }

}
