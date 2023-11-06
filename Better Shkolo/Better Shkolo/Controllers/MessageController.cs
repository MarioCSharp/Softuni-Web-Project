using Better_Shkolo.Models.Message;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.MessageService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Better_Shkolo.Controllers
{
    public class MessageController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IMessageService messageService;

        public MessageController(IAccountService accountService,
                                 IMessageService messageService)
        {
            this.accountService = accountService;
            this.messageService = messageService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await messageService.GetMeesagesAsync(accountService.GetUserId()));
        }
        [Authorize]
        public async Task<IActionResult> Send()
        {
            return View(await messageService.GenerateModel(accountService.GetUserId()));
        }
    }
}
