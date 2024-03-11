using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.GroupPermissionDTO
{
    public class GroupPermissionDTO
    {
        public bool canCreate { get; set; } = true;
        public bool canDelete { get; set; } = true;
        public bool canRead { get; set; } = true;
        public bool canUpdate { get; set; } = true;
        public string fkUserType { get; set; } = string.Empty;
        public string fkFunctionality { get; set; } = string.Empty;
    }
    public class AddGroupPermissionDTO : GroupPermissionDTO
    {
        [Required]
        public new string fkUserType { get; set; } = string.Empty;
        [Required]
        public new string fkFunctionality { get; set; } = string.Empty;
    }
    public class UpdateGroupPermissionDTO : GroupPermissionDTO
    {
        [Required]
        public string groupPermissionId { get; set; } = string.Empty;
    }
    public class GroupPermissionResponseDTO : GroupPermissionDTO
    {
        public string groupPermissionId { get; set; } = string.Empty;
        public string userTypeName { get; set; } = string.Empty;
        public string functionalityName { get; set; } = string.Empty;
    }
}
