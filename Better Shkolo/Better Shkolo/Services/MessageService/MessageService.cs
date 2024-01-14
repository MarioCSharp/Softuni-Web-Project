using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Account;
using Better_Shkolo.Models.Message;
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

            return model;
        }

        public async Task<MessageDetailsModel> GetDetailsAsync(int id)
        {
            var msg = await context.Messages.FindAsync(id);
            var user = await context.Users.FindAsync(msg.SentByUserId);

            if (msg is null || user is null)
            {
                return null;
            }

            return new MessageDetailsModel
            {
                Content = msg.Content,
                Titile = msg.Title,
                SentByUserEmail = user.Email,
                SentByUserName = user.FirstName + " " + user.LastName,
            };
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

        public async Task<bool> SendAsync(string userId, MessageSendModel model)
        {
            var user = await context.Users.FindAsync(userId);
            var sentToUser = await context.Users.FindAsync(model.SentToUserId);

            if (user is null || sentToUser is null) throw new Exception("User not found!");

            var message = new Message
            {
                SentToUserId = sentToUser.Id,
                Title = model.Title,
                Content = model.Content,
                SentByUserId = user.Id,
                Read = false
            };

            await context.Messages.AddAsync(message);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SendGradeAsync(string userId, MessageSendGradeModel model)
        {
            var users = await context.Students.Where(x => x.GradeId == model.SendGradeId).ToListAsync();

            foreach (var u in users)
            {
                var res = await SendAsync(userId, new MessageSendModel()
                {
                    Title = model.Title,
                    Content = model.Content,
                    SentToUserId = u.UserId
                });

                if (!res)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
