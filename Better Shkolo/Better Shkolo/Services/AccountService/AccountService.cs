using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Better_Shkolo.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private ApplicationDbContext context;
        private UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContext;
        public AccountService(ApplicationDbContext context,
                              UserManager<User> userManager,
                              IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContext = httpContext;

        }
        public async Task<List<UserDisplayModel>> GetAllAvailabeUsers()
        {
            var result = new List<UserDisplayModel>();

            foreach (var user in context.Users)
            {
                var roles = await userManager.GetRolesAsync(user);

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

        public string GetUserId()
        {
            return httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<bool> IsGradeTeacher()
        {
            var userId = GetUserId();

            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.UserId == userId);

            if (teacher is null)
            {
                return false;
            }

            return await context.Grades.AnyAsync(x => x.TeacherId == teacher.Id);
        }

        public async Task<bool> HasRole()
        {
            var user = await context.Users.FindAsync(GetUserId());

            if (user is null)
            {
                return false;
            }

            var roles = await userManager.GetRolesAsync(user);

            return roles.Count > 0;
        }

        public async Task<UserProfileModel> GetUser()
        {
            var user = await context.Users.FindAsync(GetUserId());

            return new UserProfileModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.Phone,
                City = user.City,
                Address = user.Address,
                Country = user.Country,
                BirthDate = user.BirthDate,
                DoctorAddress = user.DoctorAddress,
                DoctorName = user.DoctorName,
                DoctorPhone = user.DoctorPhone,
            };
        }

        public async Task<bool> EditUser(UserProfileModel model)
        {
            var user = await context.Users.FindAsync(GetUserId());

            if (user is null) return false;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Phone = model.PhoneNumber;
            user.City = model.City;
            user.Address = model.Address;
            user.Country = model.Country;
            user.BirthDate = model.BirthDate;
            if (string.IsNullOrEmpty(user.Address))
            {
                user.Address = "";
            }
            if (string.IsNullOrEmpty(user.City))
            {
                user.City = "";
            }
            if (string.IsNullOrEmpty(user.Country))
            {
                user.Country = "";
            }
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditAddress(UserAddressModel model)
        {
            var user = await context.Users.FindAsync(GetUserId());

            if (user is null) return false;

            user.Address = model.Address;
            user.City = model.City;
            user.Country = model.Country;
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditStatus(StatusEditModel model)
        {
            var user = await context.Users.FindAsync(model.UserId);

            user.Chronic = model.Status;
            await context.SaveChangesAsync();

            return user.Chronic == model.Status;
        }

        public async Task<User> GetUser(string userId)
        {
            var u = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            return u;
        }

        public async Task<bool> EditDoctor(DoctorEditModel model)
        {
            var u = await context.Users.FindAsync(model.UserId);

            if (u is null) return false;

            u.DoctorAddress = model.Address;
            await context.SaveChangesAsync();
            u.DoctorName = model.Name;
            await context.SaveChangesAsync();
            u.DoctorPhone = model.Phone;
            await context.SaveChangesAsync();

            return true;
        }
    }
}
