using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Account;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Models.Message;
using Better_Shkolo.Models.School;
using Better_Shkolo.Models.Teacher;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Better_Shkolo.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext context;
        private readonly IMemoryCache cache;
        private readonly UserManager<User> um;
        public MessageService(ApplicationDbContext context,
                              IMemoryCache cache,
                              UserManager<User> um)
        {
            this.context = context;
            this.cache = cache;
            this.um = um;
        }

        public async Task<MessageSendModel> GenerateModel(string userId)
        {
            var model = new MessageSendModel();
            var user = await context.Users.FindAsync(userId);

            model.Options = new List<int>()
            {
                0, 1, 2, 3
            };

            var users = cache.Get<List<UserDisplayModel>>("UsersForMessageService");

            if (users == null)
            {
                var temp = await context.Users
                    .Select(x => new UserDisplayModel
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,

                    }).ToListAsync();

                cache.Set("UsersForMessageService", temp);
                model.Users = temp;
            }
            else
            {
                model.Users = users;
            }

            if (await um.IsInRoleAsync(user, "Administrator") || await um.IsInRoleAsync(user, "Director"))
            {
                var grades = cache.Get<List<GradeDisplayModel>>("GradesForMessageService");

                if (grades == null)
                {

                }
                else
                {
                    model.Grades = grades;
                }
            }
            else
            {
                model.Grades = new List<GradeDisplayModel>();
                model.Schools = new List<SchoolViewModel>();
                model.Teachers = new List<TeacherDisplayModel>();
            }
            


            return model;
        }

        public async Task<List<MeesageIndexModel>> GetMeesagesAsync(string userId)
        {
            var user = await context.Users.FindAsync(userId);

            if (user is null) throw new Exception("User is not found. (GetMeesagesAsync) - MessageService");

            return await context.Messages
                .Where(x => x.SentToUserId == userId)
                .Select(x => new MeesageIndexModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    SentByUserId = x.SentByUserId,
                    Read = x.Read
                })
                .ToListAsync();
        }
    }
}
