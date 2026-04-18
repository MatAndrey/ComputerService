using ComputerService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupportController(ISupportService supportService) : Controller
    {
        [HttpGet("/admin/support")]
        [Authorize(Roles = "admin,support.view")]
        public async Task<IActionResult> Index()
        {
            var messages = await supportService.GetAllMessagesAsync();
            return View(messages);
        }
    }
}
