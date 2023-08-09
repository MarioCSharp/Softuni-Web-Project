namespace Better_Shkolo.Models.Student
{
    public class StudentStandingsModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double Success { get; set; }
        public Data.Models.Student Student { get; set; }
        public int Place { get; set; }
    }
}
