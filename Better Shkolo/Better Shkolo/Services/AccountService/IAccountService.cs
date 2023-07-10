using Better_Shkolo.Models.Account;

namespace Better_Shkolo.Services.AccountService
{
    public interface IAccountService
    {
        Task<List<UserDisplayModel>> GetAllAvailabeUsers();
        string GetUserId();
        Task<bool> IsGradeTeacher();
    }
}
