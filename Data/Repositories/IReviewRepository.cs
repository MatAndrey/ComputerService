using ComputerService.Models;

namespace ComputerService.Data.Repositories
{
    public interface IReviewRepository
    {
        IQueryable<Review> GetAll();
        Task AddAsync(Review review);
    }
}
