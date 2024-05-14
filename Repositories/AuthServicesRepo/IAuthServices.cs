using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.AuthDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.JWTServicesRepo;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BISPAPIORA.Repositories.AuthServicesRepo
{
    public interface IAuthServices
    {
        public  Task<ResponseModel<LoginResponseDTO>> Login(LoginRequestDTO model);
        public  Task<ResponseModel<LoginResponseDTO>> Logout(string logoutId);
        public  Task<ResponseModel<LoginResponseDTO>> ResetPassword(string userEmail);
    }
}
