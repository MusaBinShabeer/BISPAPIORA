using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.AppVersionDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.AppVersionServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class AppVersionController : ControllerBase
    {
        private readonly IAppVersionService appVersionService;
        public AppVersionController(IAppVersionService appVersionService)
        {
            this.appVersionService = appVersionService;
        }
        // Add New App Version Method
        [HttpPost]
        public async Task<ActionResult<ResponseModel<AppVersionResponseDTO>>> Post(AddAppVersionDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = appVersionService.AddAppVersion(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<AppVersionResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Update App Version Method
        [HttpPut]
        public async Task<ActionResult<ResponseModel<AppVersionResponseDTO>>> Put(UpdateAppVersionDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = appVersionService.UpdateAppVersion(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<AppVersionResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Delete App Version Method
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<AppVersionResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = appVersionService.DeleteAppVersion(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<AppVersionResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //To get App Version By id Method
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<AppVersionResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = appVersionService.GetAppVersion(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<AppVersionResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //To get all App Versions Method
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<AppVersionResponseDTO>>>> Get()
        {
            var response = appVersionService.GetAppVersionsList();
            return Ok(await response);
        }
    }
}
