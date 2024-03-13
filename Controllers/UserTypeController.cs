using BISPAPIORA.Models.DTOS.UserTypeDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.UserTypeServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BISPAPIORA.Extensions.Middleware;

namespace BISPAPIORA.Controllers
{
    // Controller for managing user types
    // Requires user authentication
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService userTypeService;

        // Constructor to inject the userTypeService dependency
        public UserTypeController(IUserTypeService userTypeService)
        {
            this.userTypeService = userTypeService;
        }

        // POST api/UserType
        // Endpoint for adding a new user type
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

        // PUT api/UserType
        // Endpoint for updating an existing user type
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

        // DELETE api/UserType
        // Endpoint for deleting a user type by ID
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

        // GET api/UserType/GetById
        // Endpoint for getting a user type by ID
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

        // GET api/UserType
        // Endpoint for getting a list of all user types
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<UserTypeResponseDTO>>>> Get()
        {
            var response = userTypeService.GetUserTypesList();
            return Ok(await response);
        }
    }

}
