using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace Better_Shkolo.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private ApplicationDbContext context;
        private UserManager<User> userManager;
        public AccountService(ApplicationDbContext context,
                              UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<List<UserDisplayModel>> GetAllAvailabeUsers()
        {
            var result = new List<UserDisplayModel>();


            foreach (var user in context.Users)
            {
                var roles =  await userManager.GetRolesAsync(user);

                if (roles.Count == 0)
                {
                    result.Add(new UserDisplayModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    });
                }
            }

            return result;
        }
    }
}
