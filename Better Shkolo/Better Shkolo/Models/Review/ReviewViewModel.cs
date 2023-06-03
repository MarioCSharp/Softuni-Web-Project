namespace Better_Shkolo.Models.Review
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime AddedOn { get; set; }
        public string TeacherName { get; set; }
        public int TeacherId { get; set; }
    }
}
