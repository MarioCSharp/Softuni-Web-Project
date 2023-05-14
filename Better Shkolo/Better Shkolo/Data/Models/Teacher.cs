namespace Better_Shkolo.Data.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
