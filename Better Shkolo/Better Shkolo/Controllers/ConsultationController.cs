using Better_Shkolo.Models.Consultation;
using Better_Shkolo.Services.ConsultationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
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
        public async Task<IActionResult> Create(int gradeId)
        {
            var model = new ConsultationCreateModel { GradeId = gradeId };

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
            var result = await consultationService.Analyze(gradeId, type, term);

            return View(result);
        }
    }
}
