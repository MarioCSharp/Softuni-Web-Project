using Better_Shkolo.Data;
using Better_Shkolo.Models.Message;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext context;

        public MessageService(ApplicationDbContext context)
        {
            this.context = context;
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
