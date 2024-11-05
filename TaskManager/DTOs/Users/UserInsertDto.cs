using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTOs.Users
{
    public class UserInsertDto
    {
        [Required(ErrorMessage = "Usrename is required.")]
        [MaxLength(20, ErrorMessage = "Username must be maximum 20 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        [MaxLength(20, ErrorMessage = "Firstname must be maximum 20 characters.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [MaxLength(30, ErrorMessage = "Lastname must be maximum 30 characters.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(20, ErrorMessage = "Email must be maximum 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be minimum 8 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        public string ConfirmPassword { get; set; }
    }
}
