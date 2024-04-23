namespace Better_Shkolo.Models.Team
{
    public class TeamDetailsModel
    {
        public int Id { get; set; }
        public string RoomId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string TeacherName { get; set; } = null!;
        public string GradeName { get; set; } = null!;
    }
}
