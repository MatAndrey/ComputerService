using ComputerService.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Data.Repositories
{
    public class PageRepository(AppDbContext dbContext) : IPageRepository
    {
        public async Task<Page?> GetPageAsync(int id)
        {
            return await dbContext.Pages
                .Include(p => p.Translations)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdatePageAsync(Page page)
        {
            dbContext.Pages.Update(page);
            await dbContext.SaveChangesAsync();
        }
    }
}
