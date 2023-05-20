using Better_Shkolo.Models.Account;
using Better_Shkolo.Models.Grade;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Student
{
    public class StudentCreateModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int SchoolId { get; set; }
        [Required]
        public int GradeId { get; set; }
        public int GradeTeacherId { get; set; }

        public List<UserDisplayModel> Users { get; set; }
        public List<GradeDisplayModel> Grades { get; set; }
    }
}
