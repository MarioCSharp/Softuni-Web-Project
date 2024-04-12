using BetterShkolo.Data.Models;
using BetterShkolo.Data;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.Account;
using BetterShkolo.Models.Message;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BetterShkolo.Services.MessageService
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

        public async Task<bool> Delete(string userId, int msgId)
        {
            var msg = await context.Messages.FindAsync(msgId);

            if (msg is null || msg.SentToUserId != userId) return false;

            msg.Deleted = true;
            await context.SaveChangesAsync();

            return true;
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

            msg.Read = true;
            await context.SaveChangesAsync();

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
                Deleted = msg.Deleted,
                TimeSent = msg.DateSent.ToString("MMMM dd"),
            };
        }

        public async Task<MessageIndexModel> GetMeesagesAsync(string userId)
        {
            var user = await context.Users.FindAsync(userId);

            if (user is null) throw new Exception("User is not found. (GetMeesagesAsync) - MessageService");
            var model = new MessageIndexModel();

            model.Recieved = await context.Messages
                .Where(x => x.SentToUserId == userId && !x.Deleted)
                .Select(x => new RecievedMessageModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    SentByFirstName = x.SentBy.FirstName,
                    SentByLastName = x.SentBy.LastName,
                    SentByUserEmail = x.SentBy.Email,
                    Deleted = x.Deleted,
                    TimeSent = x.DateSent.ToString("MMMM dd")
                })
                .ToListAsync();

            model.Sent = await context.Messages
                .Where(x => x.SentByUserId == userId && !x.Deleted)
                .Select(x => new SentMessageModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    SentToFirstName = x.SentToUser.FirstName,
                    SentToLastName = x.SentToUser.LastName,
                    SentToEmail = x.SentBy.Email,
                    Deleted = x.Deleted,
                    TimeSent = x.DateSent.ToString("MMMM dd")
                })
                .ToListAsync();

            model.Deleted = await context.Messages
                .Where(x => x.SentToUserId == userId && x.Deleted)
                .Select(x => new DeleteMessageModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    SentByFirstName = x.SentBy.FirstName,
                    SentByLastName = x.SentBy.LastName,
                    SentToEmail = x.SentBy.Email,
                    Deleted = x.Deleted,
                    TimeSent = x.DateSent.ToString("MMMM dd")
                })
                .ToListAsync();

            return model;
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
                Read = false,
                Deleted = false,
                DateSent = DateTime.UtcNow,
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
