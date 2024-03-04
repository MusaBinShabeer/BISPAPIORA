using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.UserTypeDTO
{
    public class UserTypeDTO
    {
        public string userTypeName { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
    }
    public class AddUserTypeDTO : UserTypeDTO
    {
        [Required]
        public new string userTypeName { get; set; } = string.Empty; 
    }
    public class UpdateUserTypeDTO : UserTypeDTO
    {
        [Required]
        public string userTypeId { get; set; } = string.Empty;
    }
    public class UserTypeResponseDTO : UserTypeDTO
    {
        public string userTypeId { get; set; } = string.Empty;
    }
}
