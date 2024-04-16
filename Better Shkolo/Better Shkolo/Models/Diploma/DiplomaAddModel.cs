using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Diploma
{
    public class DiplomaAddModel
    {
        [Required]
        [Display(Name = "Вид документ")]
        public string Type { get; set; } = null!;
        [Required]
        [Display(Name = "Учебна година")]
        public string SchoolYear { get; set; } = null!;
        [Required]
        [Display(Name = "Име на ученик")]
        public string FullName { get; set; } = null!;
        [Required]
        [Display(Name = "Идентификатор - ЕГН")]
        public string Identification { get; set; } = null!;
        [Required]
        [Display(Name = "Серия")]
        public string Series { get; set; } = null!;
        [Required]
        [Display(Name = "Фабричен №")]
        public string FabricNumber { get; set; } = null!;
        [Required]
        [Display(Name = "Общ регистрационен №")]
        public int RegistrationNumber { get; set; }
        [Required]
        [Display(Name = "Регистрационен № за годината")]
        public int YearRegistrationNumber { get; set; }
        [Required]
        [Display(Name = "Дата на издаване")]
        public DateTime IssuedDate { get; set; }
        [Required]
        [Display(Name = "Форма на обучение")]
        public string EducationForm { get; set; } = null!;
        [Display(Name = "Файл")]
        public byte[] File { get; set; } = null!;
    }
}
