using BetterShkolo.Data.Models;
using BetterShkolo.Models.Consultation;
using BetterShkolo.Models.Grade;
using QuestPDF.Fluent;
using Document = QuestPDF.Fluent.Document;

namespace BetterShkolo.Services.ConsultationService
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
