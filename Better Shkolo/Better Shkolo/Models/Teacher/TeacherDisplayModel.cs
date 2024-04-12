namespace BetterShkolo.Models.Teacher
{
    public class TeacherDisplayModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int SchoolId { get; set; }
    }
}
