using ComputerService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ComputerService.Controllers
{
    public class CartController(ICartService cartService) : Controller
    {
        [HttpGet("/cart")]
        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var items = await cartService.GetCartWithDetailsAsync(culture);
            return View(items);
        }

        [HttpGet("/cart/{id}")]
        public IActionResult CartItem(int id)
        {
            var item = cartService.GetItem(id);
            return Ok(item.Quantity);
        }

        [HttpPost("/cart/{id}")]
        public IActionResult CartItem(int id, int quantity)
        {
            var item = cartService.UpdateItem(id, quantity);
            return Ok(item.Quantity);
        }
    }
}
