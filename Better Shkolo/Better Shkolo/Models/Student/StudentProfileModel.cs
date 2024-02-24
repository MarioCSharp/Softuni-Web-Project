using Better_Shkolo.Models.Absences;
using Better_Shkolo.Models.Mark;
using Better_Shkolo.Models.Review;
using Better_Shkolo.Models.Subject;
using Better_Shkolo.Models.Test;

namespace Better_Shkolo.Models.Student
{
    public class StudentProfileModel
    {
        public string UserId { get; set; } = null!;
        public string StudentFullName { get; set; } = null!;
        public List<MarkOverallModel> Marks { get; set; } = null!;
        public List<AbsencesOverallModel> Absences { get; set; } = null!;
        public List<ReviewOverallModel> Reviews { get; set; } = null!;
        public List<TestOverallModel> Tests { get; set; } = null!;
        public Dictionary<int, (int, int)> SubjectTermMark { get; set; } = null!;
        public int Term { get; set; }

        public List<SubjectDisplayModel> AllSubjects { get; set; } = null!;
    }
}
