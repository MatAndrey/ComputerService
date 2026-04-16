using ComputerService.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Data.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<User> CreateUserAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(string login)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Login == login);
            if (user != null) 
                context.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await context.Users.Include(u => u.UserPrivileges).ToListAsync();
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await context.Users.Include(u => u.UserPrivileges).FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}
