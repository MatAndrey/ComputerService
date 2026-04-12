using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
