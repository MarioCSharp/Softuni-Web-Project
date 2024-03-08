using Better_Shkolo.Models.Table;

namespace Better_Shkolo.Services.TableService
{
    public interface ITableService
    {
        Task<bool> GenerateProgram(int schoolId);
        Task<TableViewModel> GetSchedule(int gradeId);
        Task<string> GetCurrentPeriod(string userId);
        Task<string> GetNextPeriod(string userId);
    }
}
