using ComputerService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ComputerService.Controllers
{
    public class ProductsController(ICategoryService categoryService, IProductService productService) : Controller
    {
        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var products = await productService.GetAllProductsAsync(culture, false);
            var categories = await categoryService.GetAllCategoriesAsync(culture);
            return View((products, categories));
        }

        [HttpGet("/categories/{categoryId}")]
        public async Task<IActionResult> Category(int categoryId)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var category = await categoryService.GetCategoryByIdAsync(categoryId, culture);
            if (category == null)
            {
                return NotFound();
            }
            var products = await productService.GetProductsByCategoryAsync(categoryId, culture, false);
            ViewData["CategoryName"] = category.Name;
            return View(products);
        }
    }
}
