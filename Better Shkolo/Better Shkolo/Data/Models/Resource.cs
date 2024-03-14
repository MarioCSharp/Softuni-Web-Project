using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Better_Shkolo.Data.Models
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int LessonId { get; set; }
        [ForeignKey(nameof(LessonId))]
        public Lesson Lesson { get; set; } = null!;
        public byte[] File { get; set; }
        public string Link { get; set; }
    }
}
