﻿using Better_Shkolo.Models.Standings;
using Better_Shkolo.Services.StandingsService;
using Better_Shkolo.Services.StudentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Better_Shkolo.Controllers
{
    public class StandingsController : Controller
    {
        private IStudentService studentService;
        private IStandingsService standingsService;

        public StandingsController(IStudentService studentService,
                                   IStandingsService standingsService)
        {
            this.studentService = studentService;
            this.standingsService = standingsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> View(string searchTerm, string gradeTerm, int currentPage)
        {
            var stundentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var student = await studentService.GetStudent(stundentUserId);

            var model = await standingsService.GetStandings(student, searchTerm, gradeTerm, currentPage, 2);

            return View(model);
        }
    }
}
