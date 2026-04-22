using ComputerService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ComputerService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController(IOrderService orderService) : Controller
    {
        [HttpGet("/admin/orders")]
        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var orders = await orderService.GetAllOrdersAsync(culture);
            return View(orders);
        }

        [HttpGet("/admin/orders/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var order = await orderService.GetOrderByIdAsync(culture, id);
            if (order != null)
            {
                return View(order);
            }
            return NotFound();
        }
    }
}
