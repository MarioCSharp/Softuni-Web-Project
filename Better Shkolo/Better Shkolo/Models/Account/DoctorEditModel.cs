using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Account
{
    public class DoctorEditModel
    {
        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;
        [Required]
        [Display(Name = "Телефонен номер")]
        public string Phone { get; set; } = null!;
        [Required]
        [Display(Name = "Адрес на практика")]
        public string Address { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
