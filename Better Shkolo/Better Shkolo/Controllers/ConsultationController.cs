using BetterShkolo.Models.Consultation;
using BetterShkolo.Services.ConsultationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace BetterShkolo.Controllers
{
    public class ConsultationController : Controller
    {
        private IConsultationService consultationService;
        public ConsultationController(IConsultationService consultationService)
        {
            this.consultationService = consultationService;
        }
        [HttpGet]
        [Authorize(Policy = "DirectorTeacherPolicy")]
        public async Task<IActionResult> Create()
        {
            var role = "";

            if (User.IsInRole("Director"))
            {
                role = "director";
            }
            else if (User.IsInRole("Teacher"))
            {
                role = "teacher";
            }

            var model = new ConsultationCreateModel { Grades = await consultationService.GetGrades(role) };

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "DirectorTeacherPolicy")]
        public async Task<IActionResult> Create(ConsultationCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await consultationService.Create(model);

            if (!res) return BadRequest();

            return RedirectToAction("Analyze", "Consultation", new { gradeId = model.GradeId, type = model.Type, term = model.Term });
        }
        [HttpGet]
        [Authorize(Policy = "DirectorTeacherPolicy")]
        public async Task<IActionResult> Analyze(int gradeId, string type, int term)
        {
            if (type == "Входно ниво" || type == "Писмени изпитване" || type == "Писмени изпитване"
                || type == "Устно изпитване" || type == "Контролно" || type == "Проект"
                || type == "Активно участие" || type == "Срочни оценки" || type == "Годишни оценки"
                || type == "Общо образователни предмети" || type == "Специални предмети")
            {
                type = type switch
                {
                    "Входно ниво" => "Entry",
                    "Писмени изпитване" => "Writting",
                    "Устно изпитване" => "Speaking",
                    "Контролно" => "Test",
                    "Проект" => "Project",
                    "Активно участие" => "EntActivery",
                    "Срочни оценки" => "Term",
                    "Годишни оценки" => "Year",
                    "Общо образователни предмети" => "OOP",
                    "Специални предмети" => "SPC"
                };
            }

            var result = await consultationService.Analyze(gradeId, type, term);

            return View(result);
        }
        [HttpGet]
        [Authorize(Policy = "DirectorTeacherPolicy")]
        public async Task<IActionResult> Mine()
        {
            var model = await consultationService.GetUserConsultations();

            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "DirectorTeacherPolicy")]
        public async Task<IActionResult> Delete(string userId, int gradeId, string type)
        {
            await consultationService.Delete(userId, gradeId, type);

            return RedirectToAction("Mine", "Consultation");
        }
        [HttpGet]
        [Authorize(Policy = "DirectorTeacherPolicy")]
        public async Task<IActionResult> ToPdf(string type, int gradeId, string userId, int term)
        {
            var file = await consultationService.GeneratePdf(type, gradeId, userId, term);

            var stream = new MemoryStream();
            file.GeneratePdf(stream);

            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "spravka.pdf");
        }
    }
}
