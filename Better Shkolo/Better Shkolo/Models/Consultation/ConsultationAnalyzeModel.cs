﻿namespace Better_Shkolo.Models.Consultation
{
    public class ConsultationAnalyzeModel
    {
        public string Type { get; set; } = null!;
        public string GradeName { get; set; } = null!;
        public double Average { get; set; }
        public Dictionary<string, double> SubjectByConsultation { get; set; } = null!;
    }
}
