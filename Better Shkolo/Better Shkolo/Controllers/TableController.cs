using Better_Shkolo.Services.TableService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class TableController : Controller
    {
        private ITableService tableService;
        public TableController(ITableService tableService)
        {
            this.tableService = tableService;
        }
        [Authorize(Roles = "Director")]
        [HttpGet]
        public async Task<IActionResult> Generate(int id)
        {
            var result = await tableService.GenerateProgram(id);

            return RedirectToAction("Index", "Home");
        }
    }
}
