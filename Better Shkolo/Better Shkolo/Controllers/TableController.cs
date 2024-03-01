using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.StudentService;
using Better_Shkolo.Services.TableService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
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

                gradeId = s.GradeId;
            }

            var schedule = await tableService.GetSchedule(gradeId);

            return View(schedule);
        }
    }
}
