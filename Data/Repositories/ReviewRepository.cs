using ComputerService.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Data.Repositories
{
    public class ReviewRepository(AppDbContext dbContext) : IReviewRepository
    {
        public async Task AddAsync(Review review)
        {
            dbContext.Reviews.Add(review);
            await dbContext.SaveChangesAsync();
        }
        public IQueryable<Review> GetAll()
        {
            return dbContext.Reviews;
        }
    }
}
