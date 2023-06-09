﻿using Better_Shkolo.Data;
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
    }
}
