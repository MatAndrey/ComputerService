using ComputerService.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Data.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<User?> GetByLoginAsync(string login)
        {
            return await context.Users.Include(u => u.UserPrivileges).FirstOrDefaultAsync(u => u.Login == login);
        }
    }
}
