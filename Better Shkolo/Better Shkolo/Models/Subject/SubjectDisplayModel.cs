using Better_Shkolo.Models.Teacher;

namespace Better_Shkolo.Models.Subject
{
    public class SubjectDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public List<TeacherDisplayModel> Teachers { get; set; }
    }
}
