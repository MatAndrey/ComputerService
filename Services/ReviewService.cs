using ComputerService.Data.Repositories;
using ComputerService.Models;
using ComputerService.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services
{
    public class ReviewService(IReviewRepository reviewRepository) : IReviewService
    {
        public async Task AddAsync(ReviewViewModel viewModel)
        {
            var review = new Review()
            {
                AuthorName = viewModel.AuthorName,
                Rating = viewModel.Rating,
                Text = viewModel.Text,
                Date = DateTime.Now,
            };
            await reviewRepository.AddAsync(review);
        }

        public async Task<IEnumerable<ReviewViewModel>> GetAllAsync()
        {
            return await reviewRepository
                .GetAll()
                .Select(r => new ReviewViewModel()
                {
                    AuthorName = r.AuthorName,
                    Rating = r.Rating,
                    Text = r.Text,
                    Date = r.Date,
                    Id = r.Id
                })
                .OrderByDescending(r => r.Date)
                .ToListAsync();
        }
    }
}
