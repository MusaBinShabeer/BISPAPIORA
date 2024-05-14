using BISPAPIORA.Models.DTOS.GroupPermissionDTO;
using BISPAPIORA.Models.DTOS.UserDTOs;
using System.ComponentModel.DataAnnotations;

namespace BISPAPIORA.Models.DTOS.AuthDTO
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "User Email is required")]
        public string userEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string userPassword { get; set; } = string.Empty;
       
    }   
    public class LoginResponseDTO: UserDTO
    {
        public string userId { get; set; } = string.Empty;
        public string userTypeName { get; set; } = string.Empty;
        public string userToken { get; set; } = string.Empty;
        public List<GroupPermissionResponseDTO> permissions { get; set; } = new List<GroupPermissionResponseDTO>();      
        public bool isFtpSet { get; set; } = false;             //FTP => First-TIme Password
    }
}
