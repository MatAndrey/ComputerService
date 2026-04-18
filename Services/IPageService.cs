using ComputerService.ViewModels;

namespace ComputerService.Services
{
    public interface IPageService
    {
        Task<PageViewModel> GetPageAsync(int id, string langCode);
        Task<PageViewModel> UpdatePageAsync(PageViewModel model);
    }
}
