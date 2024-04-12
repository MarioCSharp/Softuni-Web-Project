using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Account
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = null!;
        [Required]
        [PasswordPropertyText]
        [Display(Name = "Парола")]
        public string Password { get; set; } = null!;

        public string? ReturnUrl { get; set; }
    }
}
