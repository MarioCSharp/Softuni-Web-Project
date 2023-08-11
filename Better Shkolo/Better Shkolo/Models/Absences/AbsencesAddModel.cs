namespace Better_Shkolo.Models.Absence
{
    public class AbsencesAddModel
    {
        public int SubjectId { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int SchoolId { get; set; }
    }
}
