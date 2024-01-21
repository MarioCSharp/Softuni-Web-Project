using Better_Shkolo.Data;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Models.Teacher;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Subject
{
    public class SubjectCreateModel
    {
        [Required]
        [StringLength(Constants.Subject.NameMaxLength, MinimumLength = Constants.Subject.NameMinLength)]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;
        [Required]
        [Display(Name = "Учител")]
        public int TeacherId { get; set; }
        [Required]
        public int SchoolId { get; set; }
        [Required]
        [Display(Name = "Клас")]
        public int GradeId { get; set; }

        public List<TeacherDisplayModel> TeachersInSchool { get; set; }
        public List<GradeDisplayModel> GradesInSchool { get; set; }
    }
}
