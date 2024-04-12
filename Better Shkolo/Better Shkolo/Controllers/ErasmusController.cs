using BetterShkolo.Models.Erasmus;
using BetterShkolo.Models.Student;
using BetterShkolo.Services.ErasmusService;
using BetterShkolo.Services.SchoolService;
using Microsoft.AspNetCore.Mvc;

namespace BetterShkolo.Controllers
{
    public class ErasmusController : Controller
    {
        private IErasmusService erasmusService;
        private ISchoolService schoolService;
        public ErasmusController(IErasmusService erasmusService,
                                 ISchoolService schoolService)
        {
            this.erasmusService = erasmusService;
            this.schoolService = schoolService;
        }

        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var uSchoolId = await schoolService.GetSchoolIdByUser();
            var school = await schoolService.GetSchool(uSchoolId);

            var students = erasmusService.GetAligibleStudents(uSchoolId);
            var totalStudents = students.Count;

            int studentsToSkip = (currentPage - 1) * 8;

            var model = new ErasmusIndexModel()
            {
                SchoolId = uSchoolId,
                SchoolName = school.Name,
                Active = school.ActiveErasmus,
                CurrentPage = currentPage,
                StudentsPerPage = 8,
                AligibleStudents = new List<StudentDisplayModel>
                {
                    new StudentDisplayModel{FirstName = "Марио Петков"},
                    new StudentDisplayModel{FirstName = "Иван Иванов"},
                    new StudentDisplayModel{FirstName = "Алек Венков"},
                    new StudentDisplayModel{FirstName = "Ивайло Сечков"},
                    new StudentDisplayModel{FirstName = "Любослав Панталеев"},
                    new StudentDisplayModel{FirstName = "Аднрей Лазаров"},
                    new StudentDisplayModel{FirstName = "Мартин Нейков"},
                    new StudentDisplayModel{FirstName = "Преслав Цонев"},
                    new StudentDisplayModel{FirstName = "asdddd"},
                    new StudentDisplayModel{FirstName = "asddd"},
                    new StudentDisplayModel{FirstName = "asdd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asddddddddd"},
                    new StudentDisplayModel{FirstName = "asdd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asdddddd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                    new StudentDisplayModel{FirstName = "asd"},
                }.GetRange(studentsToSkip, 8),
                TotalPages = (int)Math.Ceiling((double)totalStudents / 8)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Activate(int schoolId)
        {
            await erasmusService.Activate(schoolId);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Deactivate(int schoolId)
        {
            await erasmusService.Deactivate(schoolId);

            return RedirectToAction(nameof(Index));
        }
    }
}
