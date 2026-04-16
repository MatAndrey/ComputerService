using ComputerService.Areas.Admin.ViewModels;
using ComputerService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ComputerService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController(IPageService pageService) : Controller
    {
        [HttpGet("admin/about")]
        [Authorize(Roles = "admin,about.view")]
        public async Task<IActionResult> About()
        {
            try
            {
                var culture = CultureInfo.CurrentUICulture.Name;
                var model = await pageService.GetPageAsync(1, culture);
                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("admin/about")]
        [Authorize(Roles = "admin,about.edit")]
        public async Task<IActionResult> About(string content)
        {
            try
            {
                var culture = CultureInfo.CurrentUICulture.Name;
                var model = await pageService.UpdatePageAsync(new PageViewModel
                {
                    Id = 1,
                    Content = content,
                    LangCode = culture
                });
                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("admin/contacts")]
        [Authorize(Roles = "admin,contacts.view")]
        public async Task<IActionResult> Contacts()
        {
            try
            {
                var culture = CultureInfo.CurrentUICulture.Name;
                var model = await pageService.GetPageAsync(2, culture);
                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("admin/contacts")]
        [Authorize(Roles = "admin,contacts.edit")]
        public async Task<IActionResult> Contact(string content)
        {
            try
            {
                var culture = CultureInfo.CurrentUICulture.Name;
                var model = await pageService.UpdatePageAsync(new PageViewModel
                {
                    Id = 2,
                    Content = content,
                    LangCode = culture
                });
                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("admin/history")]
        [Authorize(Roles = "admin,about.history")]
        public async Task<IActionResult> History()
        {
            try
            {
                var culture = CultureInfo.CurrentUICulture.Name;
                var model = await pageService.GetPageAsync(3, culture);
                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("admin/history")]
        [Authorize(Roles = "admin,history.edit")]
        public async Task<IActionResult> History(string content)
        {
            try
            {
                var culture = CultureInfo.CurrentUICulture.Name;
                var model = await pageService.UpdatePageAsync(new PageViewModel
                {
                    Id = 3,
                    Content = content,
                    LangCode = culture
                });
                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
