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
        public JwtSecurityToken GetToken(List<Claim> authClaims/*, bool isRememberMeActive*/)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                expires: DateTime.UtcNow.AddHours(29),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public async Task<ResponseModel<UserResponseDTO>> ValidateToken(string userToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            tokenHandler.ValidateToken(userToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                //set clockskew to zero so tokens expire exactly at token expiration time(instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            if (email != null)
            {
                var user = await db.tbl_users.Where(x => x.user_email == email && x.user_token == userToken).FirstOrDefaultAsync();
                if (user != null)
                {

                    return new ResponseModel<UserResponseDTO>()
                    {
                        data = _mapper.Map<UserResponseDTO>(user),
                        success = true
                    };
                }
                else
                {
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = false
                    };
                }
            }
            else
            {
                return new ResponseModel<UserResponseDTO>() { success = false };
            }
        }
    }
}
