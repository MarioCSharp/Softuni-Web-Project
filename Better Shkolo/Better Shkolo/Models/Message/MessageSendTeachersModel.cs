using BetterShkolo.Models.School;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Message
{
    public class MessageSendTeachersModel
    {
        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; } = null!;
        [Required]
        [Display(Name = "Съобщение")]
        public string Content { get; set; } = null!;
        [Required]
        public int SendTeacherSchoolId { get; set; }
        public List<SchoolViewModel> Schools { get; set; }
    }
}
