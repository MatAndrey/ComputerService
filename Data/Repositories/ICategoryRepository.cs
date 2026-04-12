using ComputerService.Models;

namespace ComputerService.Data.Repositories
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAllCategories();
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        Task<Category?> GetCategoryByIdAsync(int id);
    }
}
