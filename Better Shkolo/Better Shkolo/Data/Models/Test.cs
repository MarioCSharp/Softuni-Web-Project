namespace Better_Shkolo.Data.Models
{
    public class Test
    {
        public int Id { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime TestDate { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public string GradeId { get; set; }
        public Grade Grade { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; } = null!;
    }
}
