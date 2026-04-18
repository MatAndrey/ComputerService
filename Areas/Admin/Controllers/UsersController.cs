using ComputerService.Services;
using ComputerService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController(IUserService userService) : Controller
    {
        [HttpGet("/admin/users")]
        [Authorize(Roles = "admin,user.view")]
        public async Task<IActionResult> Index()
        {
            var users = await userService.GetUsersAsync();
            return View(users);
        }

        [HttpPost("/admin/users")]
        [Authorize(Roles = "admin,user.create")]
        public async Task<IActionResult> Create([FromBody] UserViewModel viewModel)
        {
            try
            {
                await userService.CreateUserAsync(viewModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/admin/users/{login}")]
        [Authorize(Roles = "admin,user.edit")]
        public async Task<IActionResult> Edit(string login)
        {
            try
            {
                var user = await userService.GetUserByLoginAsync(login);
                return View(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPost("/admin/users/{login}")]
        [Authorize(Roles = "admin,user.edit")]
        public async Task<IActionResult> Edit(string login, [FromBody] UserViewModel viewModel)
        {
            try
            {
                viewModel.Login = login;
                await userService.UpdateUserInfoAsync(viewModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/admin/users/{login}")]
        [Authorize(Roles = "admin,user.delete")]
        public async Task<IActionResult> Delete(string login)
        {
            try
            {
                await userService.DeleteUserAsync(login);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/admin/users/{login}/privileges")]
        [Authorize(Roles = "admin,user.edit")]
        public async Task<IActionResult> UpdatePrivileges(string login, List<string> privileges)
        {
            try
            {
                await userService.UpdateUserPrivileges(login, privileges);
                return RedirectToAction("Edit", new { login });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
