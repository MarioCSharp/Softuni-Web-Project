using BetterShkolo.Data.Models;
using BetterShkolo.Models.Account;
using MyTested.AspNetCore.Mvc;

namespace BetterShkolo.Services.AccountService
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
        Task<bool> EditDoctor(DoctorEditModel model);
    }
}
