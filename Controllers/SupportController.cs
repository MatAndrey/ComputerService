using ComputerService.Services;
using ComputerService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ComputerService.Controllers
{
    public class SupportController(ISupportService supportService, IStringLocalizer<SupportController> localizer) : Controller
    {
        [HttpGet("/support")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/support")]
        public async Task<IActionResult> Index(SupportMessageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await supportService.AddMessageAsync(viewModel);
                TempData["SuccessMessage"] =  localizer["Message sent successfully"].Value;
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
    }
}
