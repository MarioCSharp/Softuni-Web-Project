using Better_Shkolo.Models.Grade;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.StudyPlan
{
    public class StudyPlanChoseGradeModel
    {
        [Display(Name = "Клас")]
        public int GradeId { get; set; }
        public List<GradeDisplayModel> Grades { get; set; } = null!;
    }
}
