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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Send(MessageSendModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await messageService.SendAsync(accountService.GetUserId(), model);

            if (res == null) return BadRequest();

            return RedirectToAction("Index", "Message");
        }
    }
}
