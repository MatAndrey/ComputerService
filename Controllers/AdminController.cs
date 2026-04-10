using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
