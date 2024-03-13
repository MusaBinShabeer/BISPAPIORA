using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.GroupPermissionDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.GroupPermissionServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupPermissionController : ControllerBase
    {
        private readonly IGroupPermissionService groupPermissionService;
        public GroupPermissionController(IGroupPermissionService groupPermissionService)
        {
            this.groupPermissionService = groupPermissionService;
        }
        // Add New GroupPermission Method
        [HttpPost]
        public async Task<ActionResult<ResponseModel<GroupPermissionResponseDTO>>> Post(AddGroupPermissionDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = groupPermissionService.AddGroupPermission(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<GroupPermissionResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Update GroupPermission Method
        [HttpPut]
        public async Task<ActionResult<ResponseModel<GroupPermissionResponseDTO>>> Put(UpdateGroupPermissionDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = groupPermissionService.UpdateGroupPermission(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<GroupPermissionResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Delete GroupPermission Method
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<GroupPermissionResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = groupPermissionService.DeleteGroupPermission(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<GroupPermissionResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //To get GroupPermission By id Method
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<GroupPermissionResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = groupPermissionService.GetGroupPermission(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<GroupPermissionResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //To get all GroupPermissions Method
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<GroupPermissionResponseDTO>>>> Get()
        {
            var response = groupPermissionService.GetGroupPermissionsList();
            return Ok(await response);
        }
    }
}
