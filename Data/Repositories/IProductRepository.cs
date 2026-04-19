using ComputerService.Models;

namespace ComputerService.Data.Repositories
{
    public interface IProductRepository
    {
        Task<int> AddProductAsync(Product product);
        Task<Product?> GetProductByIdAsync(int id);
        IQueryable<Product> GetAllProducts();
        Task UpdateProductAsync(Product product);
        Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<int> products);
    }
}
