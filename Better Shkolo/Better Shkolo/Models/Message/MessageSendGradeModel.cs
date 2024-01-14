using Better_Shkolo.Models.Grade;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Message
{
    public class MessageSendGradeModel
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Content { get; set; } = null!;
        [Required]
        public int SendGradeId { get; set; }
        public List<GradeDisplayModel> Grades { get; set; }
    }
}
