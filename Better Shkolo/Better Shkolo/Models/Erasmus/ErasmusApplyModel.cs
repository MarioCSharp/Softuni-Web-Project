using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Erasmus
{
    public class ErasmusApplyModel
    {
        public int SchoolId { get; set; }
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
        [Required]
        [Display(Name = "Адрес")]
        public string Address { get; set; } = null!;
        [Display(Name = "Папка с всички необходими документи за кандидатстване (.rar или .zip)")]
        public byte[] File { get; set; } = null!;
    }
}
