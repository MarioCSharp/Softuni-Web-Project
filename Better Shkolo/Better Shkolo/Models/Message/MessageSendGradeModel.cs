using BetterShkolo.Models.Grade;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Message
{
    public class MessageSendGradeModel
    {
        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; } = null!;
        [Required]
        [Display(Name = "Съобщение")]
        public string Content { get; set; } = null!;
        [Required]
        public int SendGradeId { get; set; }
        public List<GradeDisplayModel> Grades { get; set; }
    }
}
