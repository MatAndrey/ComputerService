using ComputerService.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Data.Repositories
{
    public class NewsRepository(AppDbContext dbContext) : INewsRepository
    {
        public async Task<News> CreateNewsAsync(News news)
        {
            dbContext.News.Add(news);
            await dbContext.SaveChangesAsync();
            return news;
        }

        public async Task DeleteNewsAsync(int id)
        {
            var news = await dbContext.News.FindAsync(id);
            if(news != null)
                dbContext.News.Remove(news);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await dbContext.News
                .Include(n => n.Translations)
                .ToListAsync();
        }

        public async Task<News?> GetNewsByIdAsync(int id)
        {
            return await dbContext.News
                .Include(n => n.Translations)
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<News> UpdateNewsAsync(News news)
        {
            dbContext.News.Update(news);
            await dbContext.SaveChangesAsync();
            return news;
        }
    }
}
