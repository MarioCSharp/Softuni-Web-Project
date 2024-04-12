using BetterShkolo.Models.Teacher;

namespace BetterShkolo.Models.Subject
{
    public class SubjectDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public List<TeacherDisplayModel> Teachers { get; set; }
    }
}
