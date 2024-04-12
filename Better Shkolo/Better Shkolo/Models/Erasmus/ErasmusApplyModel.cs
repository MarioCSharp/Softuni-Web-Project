using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Erasmus
{
    public class ErasmusApplyModel
    {
        [Required]
        [Display(Name = "Име")]
        public string FullName { get; set; } = null!;
        [Required]
        [Display(Name = "ЕГН")]
        public string EGN { get; set; } = null!;
        [Required]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        [Display(Name = "Имейл")]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
