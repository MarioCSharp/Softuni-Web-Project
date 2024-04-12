using BetterShkolo.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Teacher
{
    public class TeacherCreateModel
    {
        [Required]
        [Display(Name = "Учител")]
        public string UserId { get; set; } = null!;
        public int SchoolId { get; set; }

        public List<UserDisplayModel> Users { get; set; }
    }
}
