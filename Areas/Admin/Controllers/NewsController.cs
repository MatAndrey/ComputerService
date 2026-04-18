using ComputerService.Services;
using ComputerService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ComputerService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController(INewsService newsService) : Controller
    {
        [HttpGet("/admin/news")]
        [Authorize(Roles = "admin,news.view")]
        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var newsList = await newsService.GetAllNewsAsync(culture);
            return View(newsList);
        }

        [HttpGet("/admin/news/create")]
        [Authorize(Roles = "admin,news.create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/admin/news/create")]
        [Authorize(Roles = "admin,news.create")]
        public async Task<IActionResult> Create(NewsViewModel model)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            model.LangCode = culture;
            var createdNews = await newsService.CreateNewsAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet("/admin/news/{id}")]
        [Authorize(Roles = "admin,news.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var news = await newsService.GetNewsByIdAsync(id, culture);
            if (news == null) return NotFound();
            return View(news);
        }

        [HttpPost("/admin/news/{id}")]
        [Authorize(Roles = "admin,news.edit")]
        public async Task<IActionResult> Edit(int id, NewsViewModel model)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            model.LangCode = culture;
            model.Id = id;
            var updatedNews = await newsService.UpdateNewsAsync(model);
            return RedirectToAction("Index");
        }

        [HttpDelete("/admin/news/{id}")]
        [Authorize(Roles = "admin,news.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await newsService.DeleteNewsAsync(id);
            return NoContent();
        }
    }
}
