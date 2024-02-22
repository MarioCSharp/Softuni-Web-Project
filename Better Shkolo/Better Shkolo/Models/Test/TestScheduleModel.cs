namespace Better_Shkolo.Models.Test
{
    public class TestScheduleModel
    {
        public int Id { get; set; }
        public string SubjectName { get; set; } = null!;
        public int SubjectId { get; set; }
        public DateTime TestDate { get; set; }
        public string DateWeekDay { get; set; } = null!;
    }
}
