using Better_Shkolo.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Account
{
    public class RegisterModel
    {
        [Required]
        [StringLength(Constants.User.FirstNameMaxLength, MinimumLength = Constants.User.FirstNameMinLength)]
        [Display(Name = "Собствено име")]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(Constants.User.LastNameMaxLength, MinimumLength = Constants.User.LastNameMinLength)]
        [Display(Name = "Фамилно име")]
        public string LastName { get; set; } = null!;
        [Required]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = null!;
        [Required]
        [PasswordPropertyText]
        [Compare(nameof(PasswordRepeat))]
        [Display(Name = "Парола")]
        public string Password { get; set; } = null!;
        [Required]
        [PasswordPropertyText]
        [Display(Name = "Повтори паролата")]
        public string PasswordRepeat { get; set; } = null!;
    }
}
