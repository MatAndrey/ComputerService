using ComputerService.Areas.Admin.ViewModels;
using ComputerService.Data.Repositories;
using ComputerService.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services
{
    public class ProductService(IProductRepository productRepository, IFileRepository fileRepository) : IProductService
    {
        private readonly string _imageUploadPath = "images/products";
        public async Task<int> AddProductAsync(ProductViewModel viewModel, List<IFormFile> images)
        {
            var product = new Product
            {
                CategoryId = viewModel.CategoryId,
                Price = viewModel.Price,
                Translations = [
                    new ProductTranslation
                    {
                        LangCode = viewModel.LangCode,
                        Name = viewModel.Name,
                        Description = viewModel.Description
                    }
                ],
                Images = [],
                Visible = viewModel.Visible
            };

            if(images != null)
            {
                foreach (var image in images)
                {
                    if (image.Length > 0)
                    {
                        var imageUrl = await fileRepository.SaveFileAsync(image, _imageUploadPath);
                        product.Images.Add(new ProductImage { ImageUrl = imageUrl });
                    }
                }
            }

            var id = await productRepository.AddProductAsync(product);
            return id;
        }

        public async Task<ProductViewModel?> GetProductByIdAsync(int id, string langCode)
        {
            var product = await productRepository.GetProductByIdAsync(id);
            if (product == null)
                return null;
            var translation = product.Translations.FirstOrDefault(t => t.LangCode == langCode) ??
                product.Translations.FirstOrDefault();
            return new ProductViewModel
            {
                Description = translation?.Description ?? "N/A",
                Name = translation?.Name ?? "N/A",
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Translations?.FirstOrDefault(t => t.LangCode == langCode)?.Name ?? "N/A",
                Id = product.Id,
                LangCode = translation?.LangCode ?? "N/A",
                ImageUrls = product.Images.Select(i => i.ImageUrl),
                Visible = product.Visible
            };
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsByCategoryAsync(int categoryId, string langCode)
        {
            var query = productRepository.GetAllProducts();

            var products = await query
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Translations)
                .Include(p => p.Images)
                .Include(p => p.Category)
                    .ThenInclude(c => c.Translations)
                .Select(p => new {
                    Translation = p.Translations.FirstOrDefault(t => t.LangCode == langCode) ?? p.Translations.FirstOrDefault(),
                    Product = p,
                    CategoryTranslation = p.Category.Translations.FirstOrDefault(t => t.LangCode == langCode) ?? p.Category.Translations.FirstOrDefault()
                })
                .Select(x => new ProductViewModel
                {
                    Id = x.Product.Id,
                    Name = x.Translation != null ? x.Translation.Name : "N/A",
                    Description = x.Translation != null ? x.Translation.Description : "N/A",
                    Price = x.Product.Price,
                    CategoryId = x.Product.CategoryId,
                    CategoryName = x.CategoryTranslation != null ? x.CategoryTranslation.Name : "N/A",
                    LangCode = x.Translation != null ? x.Translation.LangCode : "N/A",
                    ImageUrls = x.Product.Images.Select(i => i.ImageUrl),
                    Visible = x.Product.Visible
                })
                .ToListAsync();

            return products;
        }

        public async Task UpdateProductAsync(int id, ProductViewModel product, List<IFormFile> images)
        {
            var existingProduct = await productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
                throw new Exception("Product not found");
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Visible = product.Visible;
            var translation = existingProduct.Translations.FirstOrDefault(t => t.LangCode == product.LangCode);
            if (translation != null)
            {
                translation.Name = product.Name;
                translation.Description = product.Description;
            }
            else
            {
                existingProduct.Translations.Add(new ProductTranslation
                {
                    LangCode = product.LangCode,
                    Name = product.Name,
                    Description = product.Description
                });
            }
            var existingImageToKeep = product.ImageUrls.ToHashSet();
            foreach (var image in existingProduct.Images.ToList())
            {
                if (!existingImageToKeep.Contains(image.ImageUrl))
                {
                    fileRepository.DeleteFile(image.ImageUrl);
                    existingProduct.Images.Remove(image);
                }
            }

            if (images != null)
            {
                foreach (var image in images)
                {
                    if (image.Length > 0)
                    {
                        var imageUrl = await fileRepository.SaveFileAsync(image, _imageUploadPath);
                        existingProduct.Images.Add(new ProductImage { ImageUrl = imageUrl });
                    }
                }
            }
            await productRepository.UpdateProductAsync(existingProduct);
        }
    }
}
