using ComputerService.Models;

namespace ComputerService.Data.Repositories
{
    public interface IPageRepository
    {
        Task<Page?> GetPageAsync(int id);
        Task UpdatePageAsync(Page page);
    }
}
