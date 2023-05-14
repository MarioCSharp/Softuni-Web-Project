namespace Better_Shkolo.Data.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string DirectorId { get; set; }
        public List<Grade> Grades { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<User> Users { get; set; }
    }
}
