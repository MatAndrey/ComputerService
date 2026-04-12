using ComputerService.Services;
using ComputerService.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComputerService.Controllers
{
    public class AdminController(IUserService userService) : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/login")]
        public IActionResult Login(string returnUrl = "/")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var user = await userService.AuthenticateAsync(model.Login, model.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Login),
                        new Claim(ClaimTypes.Email, user.Email),
                    };

                    foreach (var privilege in user.UserPrivileges)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, privilege.PrivilegeName));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return LocalRedirect(returnUrl);
                }
                ModelState.AddModelError("", "Неверный логин или пароль");
            }
            return View(model);
        }

        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
