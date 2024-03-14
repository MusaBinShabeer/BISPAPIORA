using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.UserDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BISPAPIORA.Repositories.JWTServicesRepo
{
    public class JWTUtils : IJwtUtils
    {

        private readonly IConfiguration _configuration;
        private readonly Dbcontext db;
        private readonly IMapper _mapper;
        public JWTUtils(IConfiguration configuration, Dbcontext db, IMapper mapper)
        {
            _configuration = configuration;
            this.db = db;
            _mapper = mapper;
        }

        // Generates a JWT (JSON Web Token) using the specified authentication claims
        // Returns the generated JWT security token
        public JwtSecurityToken GetToken(List<Claim> authClaims/*, bool isRememberMeActive*/)
        {
            // Create a symmetric security key using the secret key specified in the configuration
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            // Create a JWT security token with specified parameters
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                expires: DateTime.UtcNow.AddHours(29),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        // Validates the provided JWT and retrieves user information from the token
        // Returns a response model containing user information if the token is valid; otherwise, returns a failure response
        public async Task<ResponseModel<UserResponseDTO>> ValidateToken(string userToken)
        {
            // Create a new JwtSecurityTokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create a symmetric security key using the secret key specified in the configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            // Validate the provided user token using token validation parameters
            tokenHandler.ValidateToken(userToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                // Set clock skew to zero so tokens expire exactly at the token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            // Cast the validated token to JwtSecurityToken
            var jwtToken = (JwtSecurityToken)validatedToken;

            // Extract the user's email from the JWT claims
            var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;

            // Check if the email is not null
            if (email != null)
            {
                // Retrieve the user from the database based on the email and user token
                var user = await db.tbl_users
                    .Where(x => x.user_email == email)
                    .FirstOrDefaultAsync();

                // Check if a user is found
                if (user != null)
                {
                    // Return a success response model with user information
                    return new ResponseModel<UserResponseDTO>()
                    {
                        data = _mapper.Map<UserResponseDTO>(user),
                        success = true
                    };
                }
                else
                {
                    // Return a failure response model if no user is found
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = false
                    };
                }
            }
            else
            {
                // Return a failure response model if the email is null
                return new ResponseModel<UserResponseDTO>() { success = false };
            }
        }

    }
}
