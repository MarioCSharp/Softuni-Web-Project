namespace Better_Shkolo.Services.TableService
{
    public interface ITableService
    {
        Task<bool> GenerateProgram(int schoolId);
    }
}
