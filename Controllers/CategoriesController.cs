using ComputerService.Services;
using ComputerService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ComputerService.Controllers
{
    [Route("/categories")]
    public class CategoriesController(ICategoryService categoryService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            IEnumerable<CategoryViewModel> categories = await categoryService.GetAllCategoriesAsync(culture);
            return View(categories);
        }

        [HttpPost]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
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
