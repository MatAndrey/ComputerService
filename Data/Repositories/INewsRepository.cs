using ComputerService.Models;

namespace ComputerService.Data.Repositories
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<News?> GetNewsByIdAsync(int id);
        Task<News> CreateNewsAsync(News news);
        Task<News> UpdateNewsAsync(News news);
        Task DeleteNewsAsync(int id);
    }
}
