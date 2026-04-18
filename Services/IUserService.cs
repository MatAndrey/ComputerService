using ComputerService.Areas.Admin.ViewModels;
using ComputerService.Models;

namespace ComputerService.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string login, string password);
        Task<IEnumerable<UserViewModel>> GetUsersAsync();
        Task<UserViewModel> CreateUserAsync(UserViewModel viewModel);
        Task<UserViewModel?> GetUserByLoginAsync(string login);
        Task<UserViewModel> UpdateUserInfoAsync(UserViewModel viewModel);
        Task<UserViewModel> UpdateUserPrivileges(string login, List<string> privileges);
        Task DeleteUserAsync(string id);
    }
}
