using ComputerService.Models;
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

        [HttpGet("/products/{id}")]
        public async Task<IActionResult> Product(int id)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var product = await productService.GetProductByIdAsync(id, culture);
            return View(product);
        }

        [HttpGet("/search")]
        public async Task<IActionResult> SearchResults(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return RedirectToAction(nameof(Index));

            var langCode = CultureInfo.CurrentUICulture.Name;
            var products = await productService.SearchProductsAsync(q, langCode, false);
            ViewData["SearchTerm"] = q;
            return View(products);
        }

        [HttpGet("search/live")]
        public async Task<IActionResult> LiveSearch(string q)
        {
            if (string.IsNullOrWhiteSpace(q) || q.Length < 2)
                return Ok(new List<ProductSearchSuggestion>());

            var langCode = CultureInfo.CurrentUICulture.Name;
            var products = await productService.SearchProductsBriefAsync(q, langCode, false);
            return Ok(products);
        }
    }
}
