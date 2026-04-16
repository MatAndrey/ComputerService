using ComputerService.Areas.Admin.ViewModels;
using ComputerService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace ComputerService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController(ICategoryService categoryService, IProductService productService) : Controller
    {
        [HttpGet("admin")]
        [Authorize(Roles = "admin,product.view")]
        public async Task<ActionResult> Index()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var products = await productService.GetAllProductsAsync(culture, true);
            return View(products);
        }

        [HttpGet("admin/categories/{categoryId:int}")]
        [Authorize(Roles = "admin,product.view")]
        public async Task<IActionResult> CategoryProducts(int categoryId)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var products = await productService.GetProductsByCategoryAsync(categoryId, culture, true);
            var category = await categoryService.GetCategoryByIdAsync(categoryId, culture);
            ViewData["categoryName"] = category != null ? category.Name : "N/A";
            return View(products);
        }

        [HttpGet("admin/products/new")]
        [Authorize(Roles = "admin,product.create")]
        public async Task<IActionResult> CreateProduct([FromQuery] int? categoryId)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var categories = await categoryService.GetAllCategoriesAsync(culture);
            ViewData["categories"] = categories;
            ViewData["selectedCategory"] = categoryId;
            return View();
        }

        [HttpPost("admin/products")]
        [Authorize(Roles = "admin,product.create")]
        public async Task<IActionResult> CreateProduct(ProductViewModel product, List<IFormFile> images)
        {
            try
            {
                var culture = CultureInfo.CurrentUICulture.Name;
                product.LangCode = culture;
                int id = await productService.AddProductAsync(product, images);
                return CreatedAtAction(nameof(Product), id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("admin/products/{id:int}")]
        [Authorize(Roles = "admin,product.view")]
        public async Task<IActionResult> Product(int id)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var product = await productService.GetProductByIdAsync(id, culture);
            if (product == null)
                return NotFound();
            var categories = await categoryService.GetAllCategoriesAsync(culture);
            ViewData["categories"] = categories;
            return View(product);
        }

        [HttpPut("admin/products/{id:int}")]
        [Authorize(Roles = "admin,product.update")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductViewModel product, [FromForm] string imageUrlsJson, List<IFormFile> images)
        {
            try
            {
                var culture = CultureInfo.CurrentUICulture.Name;
                product.LangCode = culture;
                var keptUrls = JsonSerializer.Deserialize<List<string>>(imageUrlsJson);
                product.ImageUrls = keptUrls ?? [];
                await productService.UpdateProductAsync(id, product, images);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
