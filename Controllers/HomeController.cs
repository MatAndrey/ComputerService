using ComputerService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ComputerService.Controllers
{
    public class HomeController(INewsService newsService, IPageService pageService) : Controller
    {
        [HttpGet("/news")]
        public async Task<IActionResult> News()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var news = await newsService.GetAllNewsAsync(culture);
            return View(news);
        }

        [HttpGet("/about")]
        public async Task<IActionResult> About()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var page = await pageService.GetPageAsync(1, culture);
            return View(page);
        }

        [HttpGet("/contacts")]
        public async Task<IActionResult> Contacts()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var page = await pageService.GetPageAsync(2, culture);
            return View(page);
        }

        [HttpGet("/history")]
        public async Task<IActionResult> History()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var page = await pageService.GetPageAsync(3, culture);
            return View(page);
        }

        [HttpGet("/directions")]
        public IActionResult Directions()
        {
            return View();
        }

        [HttpGet("/sitemap")]
        public IActionResult Sitemap()
        {
            return View();
        }

        [HttpGet("/access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}