using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Review
{
    public class ReviewOverallModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTime AddedOn { get; set; }
        public string SubjectName { get; set; } = null!;
        public string TeacherFullName { get; set; } = null!;
        public int SubjectId { get; set; }
    }
}
