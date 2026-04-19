using ComputerService.ViewModels;

namespace ComputerService.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewViewModel>> GetAllAsync();
        Task AddAsync(ReviewViewModel viewModel);
    }
}
