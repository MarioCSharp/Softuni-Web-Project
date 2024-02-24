using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Mark
{
    public class TermMarkAddModel
    {
        [Required]
        [Range(2, 6)]
        [Display(Name = "Оценка")]
        public int Value { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        [Required]
        [Display(Name = "Срок")]
        public byte Term { get; set; }
    }
}
