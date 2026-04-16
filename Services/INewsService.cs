using ComputerService.Areas.Admin.ViewModels;

namespace ComputerService.Services
{
    public interface INewsService
    {
        Task<IEnumerable<NewsViewModel>> GetAllNewsAsync(string langCode);
        Task<NewsViewModel> GetNewsByIdAsync(int id, string langCode);
        Task<NewsViewModel> CreateNewsAsync(NewsViewModel model);
        Task<NewsViewModel> UpdateNewsAsync(NewsViewModel model);
        Task DeleteNewsAsync(int id);
    }
}
