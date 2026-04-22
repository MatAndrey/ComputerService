using ComputerService.Services;
using ComputerService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ComputerService.Controllers
{
    public class OrdersController(IOrderService orderService) : Controller 
    {
        [HttpGet("/orders")]
        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var orders = await orderService.GetUserOrdersAsync(culture);
            return View(orders);
        }

        [HttpGet("/order")]
        public async Task<IActionResult> Order()
        {
            var orderSum = await orderService.GetOrderSumAsync(null);
            var viewModel = new OrderViewModel
            {
                Total = orderSum
            };
            return View(viewModel);
        }

        [HttpPost("/order")]
        public async Task<IActionResult> Order(OrderViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }
            await orderService.AddOrderAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }
    }
}
