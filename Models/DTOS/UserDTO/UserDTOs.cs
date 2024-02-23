using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.UserDTO
{
    public class UserDTO
    {
        public string userName { get; set; } = string.Empty;
        public string userEmail { get; set; } = string.Empty;
        public decimal userOtp { get; set; } = 0;
        public string userToken { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
    }
    public class AddUserDTO : UserDTO
    {
        [Required]
        public new string userName { get; set; } = string.Empty; 
        [Required]
        public new string userEmail { get; set; } = string.Empty;
    }
    public class UpdateUserDTO : UserDTO
    {
        [Required]
        public string userId { get; set; } = string.Empty;
    }
    public class UserResponseDTO : UserDTO
    {
        public string userId { get; set; } = string.Empty;
    }
}
