using BISPAPIORA.Models.DTOS.UserDTOs;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.UserServicesRepo
{
    public interface IUserService
    {
        public Task<ResponseModel<UserResponseDTO>> AddUser(AddUserDTO model);
        public Task<ResponseModel<UserResponseDTO>> DeleteUser(string userId);
        public Task<ResponseModel<List<UserResponseDTO>>> GetUsersList();
        public Task<ResponseModel<UserResponseDTO>> GetUser(string userId);
        public Task<ResponseModel<UserResponseDTO>> UpdateUser(UpdateUserDTO model);
    }
}
