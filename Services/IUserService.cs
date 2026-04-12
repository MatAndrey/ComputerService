using ComputerService.Models;

namespace ComputerService.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string login, string password);
    }
}
