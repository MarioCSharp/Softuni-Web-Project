using BetterShkolo.Models;
using BetterShkolo.Models.Account;
using BetterShkolo.Services.AccountService;
using BetterShkolo.Services.GradeService;
using BetterShkolo.Services.StandingsService;
using BetterShkolo.Services.StudentService;
using BetterShkolo.Services.TableService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BetterShkolo.Controllers
{
    public class HomeController : Controller
    {
        private IGradeService gradeService;
        private IStudentService studentService;
        private IStandingsService standingsService;
        private ITableService tableService;
        private IAccountService accountService;
        public HomeController(IGradeService gradeService,
                              IStudentService studentService,
                              IStandingsService standingsService,
                              ITableService tableService,
                              IAccountService accountService)
        {
            this.gradeService = gradeService;
            this.studentService = studentService;
            this.standingsService = standingsService;
            this.tableService = tableService;
            this.accountService = accountService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Teacher"))
            {
                return RedirectToAction("MyGrades", "Grade");
            }
            else if (User.IsInRole("Director"))
            {
                return RedirectToAction("Menu", "Director", new { area = "Director" });
            }
            else if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("Menu", "Admin", new { area = "Admin" });
            }

            if (User.IsInRole("Student") || User.IsInRole("Parent"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var student = await studentService.GetStudent(userId);

                if (student is null)
                {
                    student = await studentService.GetStudentFromParent(userId);
                }

                var model = await standingsService.GetPlaces(student);

                var nxt = await tableService.GetNextPeriod(student.UserId);

                if (nxt == "-")
                {
                    var table = await tableService.GetSchedule(student.GradeId);

                    var n = table.Tables.FirstOrDefault(x => x.Day == 1 && x.Period == 1);

                    if (n != null)
                    {
                        nxt = n.SubjectName;
                    }
                }

                var res = new HomeModel()
                {
                    GradeId = gradeService.GetUserGradeId().Result,
                    PlaceInSchool = model.PlaceSchool,
                    PlaceInGrade = model.PlaceGrade,
                    PlaceInYear = model.PlaceYear,
                    CurrentPeriod = await tableService.GetCurrentPeriod(accountService.GetUserId()),
                    NextPeriod = nxt,
                    SubjectInFirstPlace = await studentService.GetBestSubject(1),
                    SubjectInSecondPlace = await studentService.GetBestSubject(2),
                    SubjectInThirdPlace = await studentService.GetBestSubject(3)
                };

                return View(res);
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}