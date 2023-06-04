using Better_Shkolo.Models.Test;

namespace Better_Shkolo.Services.TestService
{
    public interface ITestService
    {
        Task<bool> Add(TestAddModel model);
        Task<List<TestDisplayModel>> GetTests();
    }
}
