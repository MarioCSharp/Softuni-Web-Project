using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Consultation;
using Better_Shkolo.Models.Grade;
using QuestPDF.Fluent;

namespace Better_Shkolo.Services.ConsultationService
{
    public interface IConsultationService
    {
        Task<bool> Create(ConsultationCreateModel model);
        Task<ConsultationAnalyzeModel> Analyze(int gradeId, string type, int term);
        Task<ConsultationAnalyzeModel> TermAnalyze(int gradeId, string type, int term);
        Task<ConsultationAnalyzeModel> DeepAnalyze(int gradeId, string type, int term);
        Task<List<ConsultationMineModel>> GetUserConsultations();
        Task<List<GradeDisplayModel>> GetGrades(string role);
        Task<bool> Delete(string userId, int gradeId, string type);
        Task<Document> GeneratePdf(string type, int gradeId, string userId, int term);
    }
}
