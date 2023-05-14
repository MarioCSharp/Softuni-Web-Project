namespace Better_Shkolo.Data.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime AddedOn { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
    }
}
