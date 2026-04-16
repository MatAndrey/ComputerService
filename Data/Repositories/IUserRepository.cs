using ComputerService.Models;

namespace ComputerService.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByLoginAsync(string login);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(string login);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
