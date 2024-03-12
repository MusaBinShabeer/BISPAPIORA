using BISPAPIORA.Models.DTOS.GroupPermissionDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.GroupPermissionServicesRepo
{
    public interface IGroupPermissionService
    {
        public Task<ResponseModel<GroupPermissionResponseDTO>> AddGroupPermission(AddGroupPermissionDTO model);
        public Task<ResponseModel<GroupPermissionResponseDTO>> DeleteGroupPermission(string groupPermissionId);
        public Task<ResponseModel<List<GroupPermissionResponseDTO>>> GetGroupPermissionsList();
        public Task<ResponseModel<GroupPermissionResponseDTO>> GetGroupPermission(string groupPermissionId);
        public Task<ResponseModel<GroupPermissionResponseDTO>> UpdateGroupPermission(UpdateGroupPermissionDTO model);
    }
}
