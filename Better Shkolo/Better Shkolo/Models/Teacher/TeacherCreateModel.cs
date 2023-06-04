using Better_Shkolo.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Teacher
{
    public class TeacherCreateModel
    {
        [Required]
        [Display(Name = "Teacher")]
        public string UserId { get; set; }
        public int SchoolId { get; set; }

        public List<UserDisplayModel> Users { get; set; }
    }
}
