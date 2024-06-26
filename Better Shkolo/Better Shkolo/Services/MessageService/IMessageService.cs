﻿using BetterShkolo.Models.Message;

namespace BetterShkolo.Services.MessageService
{
    public interface IMessageService
    {
        Task<MessageIndexModel> GetMeesagesAsync(string userId);
        Task<MessageSendModel> GenerateModel(string userId);
        Task<bool> SendAsync(string userId, MessageSendModel model);
        Task<bool> SendGradeAsync(string userId, MessageSendGradeModel model);
        Task<MessageDetailsModel> GetDetailsAsync(int id);
        Task<bool> Delete(string userId, int msgId);
    }
}
