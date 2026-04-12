using ComputerService.Data.Repositories;
using ComputerService.Models;

namespace ComputerService.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        public async Task<User?> AuthenticateAsync(string login, string password)
        {
            var user = await repository.GetByLoginAsync(login);
            if (user is null) return null;
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if(!isPasswordValid) return null;
            return user;
        }
    }
}
