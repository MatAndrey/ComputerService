using ComputerService.Data.Repositories;
using ComputerService.Models;
using ComputerService.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        public async Task AddCategoryAsync(string name, string langCode)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty", nameof(name));

            Category category = new()
            {
                Translations =
                [
                    new() {
                        Name = name,
                        LangCode = langCode
                    }
                ]
            };
            await categoryRepository.AddCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync(string langCode)
        {
            var query = categoryRepository.GetAllCategories();
            var categories = await query
                .Select(c => new
                {
                    c.Id,
                    RequestedTranslation = c.Translations.FirstOrDefault(t => t.LangCode == langCode),
                    FallbackTranslation = c.Translations.FirstOrDefault(),
                    CanDelete = !c.Products.Any()
                })
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.RequestedTranslation != null 
                        ? x.RequestedTranslation.Name 
                        : x.FallbackTranslation != null 
                            ? x.FallbackTranslation.Name 
                            : "N/A",
                    CanDelete = x.CanDelete,
                    LangCode = x.RequestedTranslation != null
                        ? langCode 
                        : x.FallbackTranslation != null 
                            ? x.FallbackTranslation.LangCode 
                            : "none"
                })
                .ToListAsync();

            return categories;
        }

        public async Task UpdateCategoryAsync(int id, string name, string langCode)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty", nameof(name));

            var category = await categoryRepository.GetCategoryByIdAsync(id) ?? throw new Exception("Category not found");
            var translation = category.Translations.FirstOrDefault(t => t.LangCode == langCode);
            if (translation != null)
            {
                translation.Name = name;
            }
            else
            {
                category.Translations.Add(new CategoryTranslation
                {
                    Name = name,
                    LangCode = langCode
                });
            }
            await categoryRepository.UpdateCategoryAsync(category);
        }
    }
}
