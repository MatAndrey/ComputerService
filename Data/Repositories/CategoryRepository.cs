using ComputerService.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Data.Repositories
{
    public class CategoryRepository(AppDbContext context) : ICategoryRepository
    {
        public async Task AddCategoryAsync(Category category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(category != null)
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }
        }

        public IQueryable<Category> GetAllCategories()
        {
            return context
                .Categories
                .Include(c => c.Translations)
                .Include(c => c.Products);
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await context
                .Categories
                .Include(c => c.Translations)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }
    }
}
