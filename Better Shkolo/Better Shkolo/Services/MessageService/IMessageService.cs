using Better_Shkolo.Models.Message;
using System.Collections.Generic;

namespace Better_Shkolo.Services.MessageService
{
    public interface IMessageService
    {
        Task<List<MeesageIndexModel>> GetMeesagesAsync(string userId);
        Task<MessageSendModel> GenerateModel(string userId);
    }
}
