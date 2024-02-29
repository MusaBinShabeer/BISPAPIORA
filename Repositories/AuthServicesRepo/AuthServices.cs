using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DTOS.AuthDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.JWTServicesRepo;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BISPAPIORA.Repositories.AuthServicesRepo
{
    public class AuthServices: IAuthServices
    {
        private readonly IConfiguration _configuration;
        private readonly Dbcontext db;
        private readonly IMapper mapper;
        private readonly IJwtUtils jwtUtils;
        public AuthServices(IConfiguration configuration, Dbcontext db, IMapper mapper, IJwtUtils jwtUtils)
        {
            _configuration = configuration;
            this.db = db;
            this.mapper = mapper;
            this.jwtUtils = jwtUtils;
        }
        public async Task<ResponseModel<LoginResponseDTO>> Login(LoginRequestDTO model)
        {
            try
            {
                var user = await db.tbl_users.Include(x => x.tbl_user_type).Where(x => x.user_email == model.userEmail).FirstOrDefaultAsync();
                if (user != null)
                {
                    if (user.user_password == new OtherServices().encodePassword(model.userPassword))
                    {
                        return await Login(user);
                    }
                    else 
                    {
                        return new ResponseModel<LoginResponseDTO>() 
                        { remarks="Password Incorrect", success= false };
                    }
                }
                else
                {
                    return new ResponseModel<LoginResponseDTO>()
                    {
                        success = false,
                        remarks = $"Login failed, user not found against email: {model.userEmail}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<LoginResponseDTO>()
                {
                    success = false,
                    remarks = $"Login failed, there was a fatal error {ex.Message.ToString()}"
                };
            }
        }
        private async Task<ResponseModel<LoginResponseDTO>> Login(tbl_user user)
        {
            var userRole = user.tbl_user_type.user_type_name;
            var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, user.user_email),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                        };
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            var token = jwtUtils.GetToken(authClaims);
            user.user_token = new JwtSecurityTokenHandler().WriteToken(token);
            await db.SaveChangesAsync();
            var responseUser = new LoginResponseDTO();
            responseUser = mapper.Map<LoginResponseDTO>(user);
            responseUser.userToken = user.user_token;
            return new ResponseModel<LoginResponseDTO>()
            {
                data = responseUser,
                success = true,
                remarks = $"Success"
            };
        }
        public async Task<ResponseModel<LoginResponseDTO>> Logout(string logoutId)
        {
            ResponseModel<LoginResponseDTO> response = new ResponseModel<LoginResponseDTO>();
            try
            {

                if (!String.IsNullOrEmpty(logoutId))
                {
                    Guid userId = Guid.Parse(logoutId);
                    var tenantUser = db.tbl_users.Where(x => x.user_id == userId).FirstOrDefault();
                    if (tenantUser != null)
                    {
                        tenantUser.user_token = "";
                        await db.SaveChangesAsync();

                        response = new ResponseModel<LoginResponseDTO>()
                        {
                            success = true,
                            remarks = $"Success"
                        };
                    }
                    else
                    {
                        response = new ResponseModel<LoginResponseDTO>()
                        {
                            success = false,
                            remarks = $"User not found"
                        };
                    }

                }
                else
                {
                    response.success = false;
                    response.remarks = "User Id is required";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.remarks = ex.Message;
            }
            return response;
        }
    }
}
