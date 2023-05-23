using Better_Shkolo.Data;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Mark
{
    public class MarkAddModel
    {
        [Required]
        [Range(Constants.Mark.MarkMinValue, Constants.Mark.MarkMaxValue
            , ErrorMessage = "The mark must be in range {0} to {1}")]
        public int Value { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int SubjectId { get; set; }
    }
}
