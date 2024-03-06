using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DTOS.AuthDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.UserDTOs;
using BISPAPIORA.Repositories.AuthServicesRepo;
using BISPAPIORA.Repositories.InnerServicesRepo;
using BISPAPIORA.Repositories.UserServicesRepo;
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
        IInnerServices innerServices;
        IUserService userService;
        OtherServices otherServices = new OtherServices();
        public AuthController(IAuthServices manager, IInnerServices innerServices, IUserService userService)
        {
            authManager = manager;
            this.innerServices = innerServices;
            this.userService = userService;
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
        [HttpPost]
        [Route("SendOtp")]
        public async Task<ActionResult<ResponseModel<LoginResponseDTO>>> SendOtp(string to)
        {
            if (ModelState.IsValid)
            {
                var otp = otherServices.GenerateOTP(4);
                var response = await innerServices.SendEmail(to,"OTP", otp);
                var userResposne = await userService.UpdateUserOtp(to, otp);
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
        [Route("VerifyOTP")]
        public async Task<ActionResult<ResponseModel<UserResponseDTO>>> VerifyOTP(string to,string otp)
        {
            if (ModelState.IsValid)
            {
                var userResposne = await userService.VerifyUserOtp(to, otp);
                if (userResposne.success == true)
                {
                    return Ok(userResposne);
                }
                else
                {
                    return Ok(userResposne);
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

    }
}
