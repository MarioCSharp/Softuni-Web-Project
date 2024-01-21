using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Test
{
    public class TestAddModel
    {
        [Required]
        [Display(Name = "Дата")]
        public DateTime TestDate { get; set; }

        public int SubjectId { get; set; }
    }
}
