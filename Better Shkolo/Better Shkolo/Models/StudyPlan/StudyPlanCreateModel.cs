namespace Better_Shkolo.Models.StudyPlan
{
    public class StudyPlanCreateModel
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public int Amount { get; set; }
    }
}
