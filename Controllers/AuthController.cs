using BISPAPIORA.Models.DTOS.AuthDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.AuthServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthServices authManager;
        public AuthController(IAuthServices manager)
        {
            authManager = manager;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<ResponseModel<LoginResponseDTO>>> Login([FromBody] LoginRequestDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await authManager.Login(model);
                if (response.success == true)
                {
                    return Ok(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            else
            {
                return BadRequest(new ResponseModel<LoginResponseDTO>()
                {
                    remarks = "Request is not valid",
                    success = false
                });
            }
        }
        [HttpGet]
        [Route("logout")]
        public async Task<ActionResult<ResponseModel<LoginResponseDTO>>> Logout(string id)
        {
            if (ModelState.IsValid)
            {
                var response = await authManager.Logout(id);
                if (response.success == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            else
            {
                return BadRequest(new ResponseModel<LoginResponseDTO>()
                {
                    remarks = "Request is not valid",
                    success = false
                });
            }
        }
    }
}
