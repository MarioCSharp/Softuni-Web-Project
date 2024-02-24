using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Mark
{
    public class YearMarkAddModel
    {
        [Required]
        [Range(2, 6)]
        public int Value { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
    }
}
