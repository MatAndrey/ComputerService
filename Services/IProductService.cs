using ComputerService.Areas.Admin.ViewModels;

namespace ComputerService.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync(string langCode, bool showInvisible);
        Task<IEnumerable<ProductViewModel>> GetProductsByCategoryAsync(int categoryId, string langCode, bool showInvisible);
        Task<ProductViewModel?> GetProductByIdAsync(int id, string langCode);
        Task<int> AddProductAsync(ProductViewModel product, List<IFormFile> images);
        Task UpdateProductAsync(int id, ProductViewModel product, List<IFormFile> images);
    }
}
