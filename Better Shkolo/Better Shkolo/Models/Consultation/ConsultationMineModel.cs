namespace Better_Shkolo.Models.Consultation
{
    public class ConsultationMineModel
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string GradeName { get; set; } = null!;
        public int SubjectId { get; set; }
        public string UserId { get; set; } = null!;
        public int GradeId { get; set; }
        public int Term { get; set; }
    }
}
