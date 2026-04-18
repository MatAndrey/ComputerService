using ComputerService.ViewModels;

namespace ComputerService.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync(string langCode);
        Task AddCategoryAsync(string name, string langCode);
        Task UpdateCategoryAsync(int id, string name, string langCode);
        Task DeleteCategoryAsync(int id);
        Task<CategoryViewModel?> GetCategoryByIdAsync(int categoryId, string culture);
    }
}
