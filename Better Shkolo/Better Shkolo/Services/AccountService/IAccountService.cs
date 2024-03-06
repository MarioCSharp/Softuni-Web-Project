using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Account;
using MyTested.AspNetCore.Mvc;

namespace Better_Shkolo.Services.AccountService
{
    public interface IAccountService
    {
        Task<List<UserDisplayModel>> GetAllAvailabeUsers();
        string GetUserId();
        Task<bool> IsGradeTeacher();
        Task<bool> HasRole();
        Task<UserProfileModel> GetUser();
        Task<User> GetUser(string userId);
        Task<bool> EditUser(UserProfileModel model);
        Task<bool> EditAddress(UserAddressModel model);
        Task<bool> EditStatus(StatusEditModel model);
    }
}
