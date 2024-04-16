using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Erasmus;
using BetterShkolo.Models.Erasmus;
using BetterShkolo.Models.Student;

namespace BetterShkolo.Services.ErasmusService
{
    public interface IErasmusService
    {
        List<StudentDisplayModel> GetAligibleStudents(int schoolId);
        Task Activate(int schoolId);
        Task Deactivate(int schoolId);
        Task<bool> Apply(ErasmusApplyModel model, IFormFile file);
        Task<bool> SchoolIsActive(int schoolId);
        Task<bool> AddDocument(ErasmusDocumentAddModel model, IFormFile file);
        Task<List<ErasmusDocumentIndexModel>> GetSchoolDocuments(int schoolId);
        Task<ErasmusDocument> GetDoc(int documentId);
        Task<List<ErasmusApplicationsModel>> GetSchoolApplications(int schoolId);
        Task<ErasmusApplication> GetApplication(int applicationId);
        Task Evaluate(ErasmusApplicationEvaluationModel evaluationModel);
    }
}
