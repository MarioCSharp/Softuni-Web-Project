namespace BetterShkolo.Models.Account
{
    public class HomeModel
    {
        public string UserId { get; set; } = null!;
        public int GradeId { get; set; }
        public int PlaceInSchool { get; set; }
        public int PlaceInGrade { get; set; }
        public int PlaceInYear { get; set; }
        public string CurrentPeriod { get; set; } = null!;
        public string NextPeriod { get; set; } = null!;
        public (string, double) SubjectInFirstPlace { get; set; }
        public (string, double) SubjectInSecondPlace { get; set; }
        public (string, double) SubjectInThirdPlace { get; set; }
    }
}
