using ComputerService.ViewModels;

namespace ComputerService.Services
{
    public interface ISupportService
    {
        Task AddMessageAsync(SupportMessageViewModel viewModel);
        Task<IEnumerable<SupportMessageViewModel>> GetAllMessagesAsync();
    }
}
