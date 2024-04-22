using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.UserDTOs;

namespace BISPAPIORA.Repositories.UserServicesRepo
{
    public interface IUserService
    {
        public Task<ResponseModel<UserResponseDTO>> AddUser(AddUserDTO model);
        public Task<ResponseModel<UserResponseDTO>> DeleteUser(string userId);
        public Task<ResponseModel<List<UserResponseDTO>>> GetUsersList();
        public Task<ResponseModel<UserResponseDTO>> GetUser(string userId);
        public Task<ResponseModel<UserResponseDTO>> UpdateUser(UpdateUserDTO model);
        public Task<ResponseModel<UserResponseDTO>> UpdateUserOtp(string to, string otp);
        public Task<ResponseModel<UserResponseDTO>> UpdateFTP(UpdateUserFtpDTO model);
        public Task<ResponseModel<UserResponseDTO>> VerifyUserOtp(string to, string otp);
    }
}
