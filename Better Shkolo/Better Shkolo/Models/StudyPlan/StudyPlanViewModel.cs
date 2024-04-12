namespace BetterShkolo.Models.StudyPlan
{
    public class StudyPlanViewModel
    {
        public int SchoolId { get; set; }
        public List<StudyPlanDisplayModel> StudyPlans { get; set; } = null!;
    }
}
