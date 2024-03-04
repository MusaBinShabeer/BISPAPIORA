using BISPAPIORA.Models.DTOS.UserTypeDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.UserTypeServicesRepo
{
    public interface IUserTypeService
    {
        public Task<ResponseModel<UserTypeResponseDTO>> AddUserType(AddUserTypeDTO model);
        public Task<ResponseModel<UserTypeResponseDTO>> DeleteUserType(string userTypeId);
        public Task<ResponseModel<List<UserTypeResponseDTO>>> GetUserTypesList();
        public Task<ResponseModel<UserTypeResponseDTO>> GetUserType(string userTypeId);
        public Task<ResponseModel<UserTypeResponseDTO>> UpdateUserType(UpdateUserTypeDTO model);
    }
}
