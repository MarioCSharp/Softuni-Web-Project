namespace Better_Shkolo.Models.Activity
{
    public class ActivityDisplayModel
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public string Month { get; set; } = null!;
        public string WeekDay { get; set; } = null!;
        public string Time { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ActivityName { get; set; } = null!;
    }
}
