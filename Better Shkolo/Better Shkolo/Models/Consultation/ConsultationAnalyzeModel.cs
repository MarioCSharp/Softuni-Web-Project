namespace Better_Shkolo.Models.Consultation
{
    public class ConsultationAnalyzeModel
    {
        public string Type { get; set; } = null!;
        public string GradeName { get; set; } = null!;
        public Dictionary<string, double> SubjectByConsultation { get; set; } = null!;
    }
}
