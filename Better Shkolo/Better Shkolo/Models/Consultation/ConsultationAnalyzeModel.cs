namespace BetterShkolo.Models.Consultation
{
    public class ConsultationAnalyzeModel
    {
        public string Type { get; set; } = null!;
        public string GradeName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public double Average { get; set; }
        public int GradeId { get; set; }
        public int Term { get; set; }
        public Dictionary<string, double> SubjectByConsultation { get; set; } = null!;
    }
}
