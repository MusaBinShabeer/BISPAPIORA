using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.UserDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BISPAPIORA.Repositories.JWTServicesRepo
{
    public interface IJwtUtils
    {
        public JwtSecurityToken GetToken(List<Claim> authClaims);
        public Task<ResponseModel<UserResponseDTO>> ValidateToken(string userToken);
    }
}