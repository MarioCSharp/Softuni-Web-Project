using Better_Shkolo.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Account
{
    public class RegisterModel
    {
        [Required]
        [StringLength(Constants.User.FirstNameMaxLength, MinimumLength = Constants.User.FirstNameMinLength)]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(Constants.User.LastNameMaxLength, MinimumLength = Constants.User.LastNameMinLength)]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [PasswordPropertyText]
        [Compare(nameof(PasswordRepeat))]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;
        [Required]
        [PasswordPropertyText]
        [Display(Name = "Repeat password")]
        public string PasswordRepeat { get; set; } = null!;
    }
}
