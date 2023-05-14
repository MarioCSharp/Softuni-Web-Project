namespace Better_Shkolo.Data.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string GradeName { get; set; }
        public string GradeSpecialty { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
    }
}
