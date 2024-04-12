using BetterShkolo.Models.Grade;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.StudyPlan
{
    public class StudyPlanChoseGradeModel
    {
        [Display(Name = "Клас")]
        public int GradeId { get; set; }
        public List<GradeDisplayModel> Grades { get; set; } = null!;
    }
}
