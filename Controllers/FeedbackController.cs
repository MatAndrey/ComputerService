using ComputerService.Services;
using ComputerService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ComputerService.Controllers
{
    public class FeedbackController(IReviewService reviewService) : Controller
    {
        [HttpGet("/feedback")]
        public async Task<IActionResult> Index()
        {
            var reviews = await reviewService.GetAllAsync();
            return View(reviews);
        }

        [HttpGet("/feedback/review")]
        public IActionResult Review()
        {
            return View();
        }

        [HttpPost("/feedback/review")]
        public async Task<IActionResult> Review(ReviewViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await reviewService.AddAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
    }
}
