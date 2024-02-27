using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Consultation;

namespace Better_Shkolo.Services.ConsultationService
{
    public interface IConsultationService
    {
        Task<bool> Create(ConsultationCreateModel model);
        Task<ConsultationAnalyzeModel> Analyze(int gradeId, string type, int term);
        Task<ConsultationAnalyzeModel> TermAnalyze(int gradeId, string type, int term);
    }
}
