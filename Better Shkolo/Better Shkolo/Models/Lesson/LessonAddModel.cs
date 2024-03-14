using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Lesson
{
    public class LessonAddModel
    {
        [Required]
        [Display(Name = "Тема")]
        public string Name { get; set; } = null!;
        public int SubjectId { get; set; }
    }
}
