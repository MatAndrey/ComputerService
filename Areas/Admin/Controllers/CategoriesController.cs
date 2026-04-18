using ComputerService.Services;
using ComputerService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ComputerService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController(ICategoryService categoryService) : Controller
    {
        [HttpGet("admin/categories")]
        [Authorize(Roles = "admin,category.view")]
        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            IEnumerable<CategoryViewModel> categories = await categoryService.GetAllCategoriesAsync(culture);
            return View(categories);
        }

        [HttpPost("admin/categories")]
        [Authorize(Roles = "admin,category.create")]
        public async Task<IActionResult> Create(string name)
        {
            var culture = CultureInfo.CurrentUICulture.Name;

            try
            {
                await categoryService.AddCategoryAsync(name, culture);
                return Created();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("admin/categories/{id:int}")]
        [Authorize(Roles = "admin,category.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }

        [HttpPut("admin/categories/{id:int}")]
        [Authorize(Roles = "admin,category.edit")]
        public async Task<IActionResult> Update(int id, string name)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            try
            {
                await categoryService.UpdateCategoryAsync(id, name, culture);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
