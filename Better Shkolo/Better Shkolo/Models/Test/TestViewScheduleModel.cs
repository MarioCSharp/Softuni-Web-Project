namespace BetterShkolo.Models.Test
{
    public class TestViewScheduleModel
    {
        public int WeekBack { get; set; }
        public int WeekForward { get; set; }
        public int GradeId { get; set; }
        public List<TestScheduleModel> Tests { get; set; } = null!;
    }
}
