using BetterShkolo.Models.Account;
using BetterShkolo.Models.Grade;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Student
{
    public class StudentCreateModel
    {
        [Required]
        [Display(Name = "Потребител")]
        public string UserId { get; set; } = null!;
        [Required]
        [Display(Name = "Училище")]
        public int SchoolId { get; set; }
        [Required]
        [Display(Name = "Клас")]
        public int GradeId { get; set; }
        public int GradeTeacherId { get; set; }
        [Required]
        [Display(Name = "Име на личен лекар")]
        public string DoctorName { get; set; } = null!;
        [Required]
        [Display(Name = "Тел. номер на личен лекар")]
        public string DoctorPhone { get; set; } = null!;
        [Required]
        [Display(Name = "Адрес на личен лекар")]
        public string DoctorAddress { get; set; } = null!;

        public List<UserDisplayModel> Users { get; set; }
        public List<GradeDisplayModel> Grades { get; set; }
    }
}
