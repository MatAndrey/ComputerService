using ComputerService.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Data.Repositories
{
    public class ProductRepository(AppDbContext dbContext) : IProductRepository
    {
        public async Task<int> AddProductAsync(Product product)
        {
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return product.Id;
        }

        public IQueryable<Product> GetAllProducts()
        {
            return dbContext.Products;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await dbContext.Products
                .Include(p => p.Images)
                .Include(p => p.Translations)
                .Include(p => p.Category)
                    .ThenInclude(c => c.Translations)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync();
        }
    }
}
