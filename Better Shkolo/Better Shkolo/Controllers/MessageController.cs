using BetterShkolo.Models.Grade;
using BetterShkolo.Models.Message;
using BetterShkolo.Services.AccountService;
using BetterShkolo.Services.GradeService;
using BetterShkolo.Services.MessageService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterShkolo.Controllers
{
    public class MessageController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IMessageService messageService;
        private readonly IGradeService gradeService;

        public MessageController(IAccountService accountService,
                                 IMessageService messageService,
                                 IGradeService gradeService)
        {
            this.accountService = accountService;
            this.messageService = messageService;
            this.gradeService = gradeService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await messageService.GetMeesagesAsync(accountService.GetUserId()));
        }
        [Authorize]
        public async Task<IActionResult> Sent()
        {
            return View(await messageService.GetMeesagesAsync(accountService.GetUserId()));
        }
        [Authorize]
        public async Task<IActionResult> Deleted()
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
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var msg = await messageService.GetDetailsAsync(id);

            if (msg is null)
            {
                return BadRequest();
            }

            return View(msg);
        }
        [HttpGet]
        [Authorize(Policy = "AdministratorDirectorTeacherPolicy")]
        public async Task<IActionResult> SendGrade()
        {
            var grades = new List<GradeDisplayModel>();

            if (User.IsInRole("Administrator"))
            {
                grades = await gradeService.GetAllGradesAsync();
            }
            else if (User.IsInRole("Director"))
            {
                grades = await gradeService.GetSchoolGradesAsync();
            }
            else if (User.IsInRole("Teacher"))
            {
                grades = await gradeService.GetTeacherGradesAsync();
            }
            else
            {
                return BadRequest();
            }

            if (grades is null)
            {
                return BadRequest();
            }

            return View(new MessageSendGradeModel()
            {
                Grades = grades
            });
        }
        [HttpPost]
        [Authorize(Policy = "AdministratorDirectorTeacherPolicy")]
        public async Task<IActionResult> SendGrade(MessageSendGradeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await messageService.SendGradeAsync(accountService.GetUserId(), model);

            if (!res) return BadRequest();

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await messageService.Delete(accountService.GetUserId(), id);

            if (!result) return BadRequest();

            return RedirectToAction(nameof(Index));
        }
    }
}
