using BetterShkolo.Models.Absences;
using BetterShkolo.Models.Mark;
using BetterShkolo.Models.Review;
using BetterShkolo.Models.Subject;
using BetterShkolo.Models.Test;

namespace BetterShkolo.Models.Student
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
