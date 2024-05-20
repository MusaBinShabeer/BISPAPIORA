using BISPAPIORA.Extensions;
using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.AuthDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.UserDTOs;
using BISPAPIORA.Repositories.AuthServicesRepo;
using BISPAPIORA.Repositories.CitizenServicesRepo;
using BISPAPIORA.Repositories.InnerServicesRepo;
using BISPAPIORA.Repositories.UserServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthServices authManager;
        IInnerServices innerServices;
        IUserService userService;
        ICitizenService citizenService;
        OtherServices otherServices = new OtherServices();
        //Constructor
        public AuthController(IAuthServices manager, IInnerServices innerServices, IUserService userService, ICitizenService citizenService )
        {
            authManager = manager;
            this.innerServices = innerServices;
            this.userService = userService;
            this.citizenService = citizenService;
        }
        //Login Method
        [AllowAnonymous]
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
        //Logout Method
        [AllowAnonymous]
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
        //Send Otp through Email
        [AllowAnonymous]
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
        [AppVersion]
        [HttpGet]
        [Route("ResetPassword")]
        public async Task<ActionResult<ResponseModel<LoginResponseDTO>>> ResetPassword(string userEmail)
        {
            if (!string.IsNullOrEmpty(userEmail))
            {
                var response = await authManager.ResetPassword(userEmail);
                return Ok(response);
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
        //Verifing OTP 
        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpPost]
        [Route("UpdatePMT")]
        public async Task<bool> UpdatePMT()
        {

            var userResposne = await citizenService.UpdatePmt();
               return true;
           
        }

    }
}
