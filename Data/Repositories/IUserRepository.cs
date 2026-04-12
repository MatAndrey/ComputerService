using ComputerService.Models;

namespace ComputerService.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByLoginAsync(string login);
    }
}
