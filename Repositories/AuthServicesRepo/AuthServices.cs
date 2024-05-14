using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DTOS.AuthDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.ComplexMappersRepo;
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
        private readonly OtherServices otherServices= new OtherServices();
        private readonly IComplexMapperServices complexMapperServices;
        public AuthServices(IConfiguration configuration, Dbcontext db, IMapper mapper, IJwtUtils jwtUtils, IComplexMapperServices complexMapperServices)
        {
            _configuration = configuration;
            this.db = db;
            this.mapper = mapper;
            this.jwtUtils = jwtUtils;
            this.complexMapperServices = complexMapperServices;
        }
        // Handles user login based on the provided credentials
        // Returns a response model indicating the success or failure of the login operation
        public async Task<ResponseModel<LoginResponseDTO>> Login(LoginRequestDTO model)
        {
            try
            {
                // Retrieve user information from the database based on the provided email
                var user = await db.tbl_users.Include(x => x.tbl_user_type).ThenInclude(x => x.tbl_group_permissions).ThenInclude(x => x.tbl_functionality).Where(x => x.user_email.ToLower() == model.userEmail.ToLower()).FirstOrDefaultAsync();

                if (user != null)
                {
                    // Check if the provided password matches the stored encoded password
                    if (user.user_password == new OtherServices().encodePassword(model.userPassword))
                    {
                        //If the password is correct, proceed with the login operation
                        return await Login(user);
                    }
                    else
                    {
                        // If the password is incorrect, return a failure response
                        return new ResponseModel<LoginResponseDTO>()
                        {
                            remarks = "Invalid Email/Password",
                            success = false
                        };
                    }
                }
                else
                {
                    // If no user is found with the provided email, return a failure response
                    return new ResponseModel<LoginResponseDTO>()
                    {
                        success = false,
                        remarks = "Invalid Email/Password"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs during login
                return new ResponseModel<LoginResponseDTO>()
                {
                    success = false,
                    remarks = $"Login failed, there was a fatal error {ex.Message.ToString()}"
                };
            }
        }

        // Handles the actual login process after successful authentication
        // Returns a response model containing the user information and a JWT token
        private async Task<ResponseModel<LoginResponseDTO>> Login(tbl_user user)
        {
            try
            {
                // Extract user role and create authentication claims
                var userRole = user.tbl_user_type.user_type_name;
                var authClaims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, user.user_email),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(ClaimTypes.Role, userRole)
                            };

                // Generate a JWT token using the authentication claims
                var token = jwtUtils.GetToken(authClaims);

                // Map user information to the response DTO
                var responseUser = complexMapperServices.ComplexAutomapperForLogin().Map<LoginResponseDTO>(user);
                responseUser.userToken = new JwtSecurityTokenHandler().WriteToken(token);
                user.user_token = responseUser.userToken;
                await db.SaveChangesAsync();

                // Return a success response model with the user information and JWT token
                return new ResponseModel<LoginResponseDTO>()
                {
                    data = responseUser,
                    success = true,
                    remarks = "Success"
                };
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs during the login process
                return new ResponseModel<LoginResponseDTO>()
                {
                    success = false,
                    remarks = $"There was an error during the login process: {ex.Message}"
                };
            }
        }

        // Handles user logout based on the provided logout ID (user ID)
        // Returns a response model indicating the success or failure of the logout operation
        public async Task<ResponseModel<LoginResponseDTO>> Logout(string logoutId)
        {
            ResponseModel<LoginResponseDTO> response = new ResponseModel<LoginResponseDTO>();
            try
            {
                // Check if the provided logout ID is not empty
                if (!String.IsNullOrEmpty(logoutId))
                {
                    // Parse the logout ID to a Guid
                    Guid userId = Guid.Parse(logoutId);

                    // Retrieve the user from the database based on the user ID
                    var tenantUser = db.tbl_users.Where(x => x.user_id == userId).FirstOrDefault();

                    if (tenantUser != null)
                    {
                        // If the user is found, clear the user token (perform logout) and save changes to the database
                        tenantUser.user_token = "";
                        await db.SaveChangesAsync();

                        // Return a success response model indicating the successful logout
                        response = new ResponseModel<LoginResponseDTO>()
                        {
                            success = true,
                            remarks = "Success"
                        };
                    }
                    else
                    {
                        // If no matching user is found, return a failure response
                        response = new ResponseModel<LoginResponseDTO>()
                        {
                            success = false,
                            remarks = "User not found"
                        };
                    }
                }
                else
                {
                    // If the logout ID is empty, return a failure response
                    response.success = false;
                    response.remarks = "User Id is required";
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs during logout
                response.success = false;
                response.remarks = ex.Message;
            }
            return response;
        }
        public async Task<ResponseModel<LoginResponseDTO>> ResetPassword(string userEmail)
        {
            try
            {
                // Retrieve user information from the database based on the provided email
                var user = await db.tbl_users
                    .Include(x => x.tbl_user_type).ThenInclude(x => x.tbl_group_permissions).ThenInclude(x => x.tbl_functionality)
                    .Where(x => x.user_email.ToLower() == userEmail.ToLower()).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.user_password= otherServices.encodePassword("12345");
                    user.is_ftp_set = false;
                    await db.SaveChangesAsync();
                    return  new ResponseModel<LoginResponseDTO>()
                    {
                        remarks = $"Password has been Resetted",
                        success = true,
                    };
                }
                else
                {
                    // If no user is found with the provided email, return a failure response
                    return new ResponseModel<LoginResponseDTO>()
                    {
                        success = false,
                        remarks = "Invalid Email/Password"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs during login
                return new ResponseModel<LoginResponseDTO>()
                {
                    success = false,
                    remarks = $"Login failed, there was a fatal error {ex.Message.ToString()}"
                };
            }
        }
    }
}
