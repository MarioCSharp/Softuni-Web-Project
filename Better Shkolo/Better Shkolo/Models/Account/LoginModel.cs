using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Account
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;

        public string? ReturnUrl { get; set; }
    }
}
