using BetterShkolo.Services.AccountService;
using BetterShkolo.Services.StudentService;
using BetterShkolo.Services.TableService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterShkolo.Controllers
{
    public class TableController : Controller
    {
        private ITableService tableService;
        private IAccountService accountService;
        private IStudentService studentService;
        public TableController(ITableService tableService,
                               IAccountService accountService,
                               IStudentService studentService)
        {
            this.tableService = tableService;
            this.accountService = accountService;
            this.studentService = studentService;
        }
        [Authorize(Roles = "Director")]
        [HttpGet]
        public async Task<IActionResult> Generate(int id)
        {
            var result = await tableService.GenerateProgram(id);

            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Schedule(int gradeId)
        {
            if (gradeId == 0)
            {
                var u = accountService.GetUserId();

                var s = await studentService.GetStudent(u);

                if (s is null)
                {
                    s = await studentService.GetStudentFromParent(u);
                }

                gradeId = s.GradeId;
            }

            var schedule = await tableService.GetSchedule(gradeId);

            return View(schedule);
        }
    }
}
