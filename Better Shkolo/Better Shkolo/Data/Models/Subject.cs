namespace Better_Shkolo.Data.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}
