using ComputerService.Data.Repositories;
using ComputerService.Models;
using ComputerService.ViewModels;
using System.Text.Json;

namespace ComputerService.Services
{
    public class CartService(
        IHttpContextAccessor httpContextAccessor,
        IProductRepository productRepository
        ) : ICartService
    {
        private ISession Session = httpContextAccessor.HttpContext?.Session ?? throw new Exception("HttpContext not found");
        private readonly string CartSessionKey = "Cart";
        private Cart GetCart()
        {
            var value = Session.GetString(CartSessionKey);
            return value == null ? new Cart() { Items = [] } : JsonSerializer.Deserialize<Cart>(value);
        }

        private void SaveCart(Cart cart)
        {
            Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
        }

        public void ClearCart()
        {
            SaveCart(new Cart() { Items = [] });
        }

        public async Task<List<CartItemViewModel>> GetCartWithDetailsAsync(string langCode)
        {
            var cart = GetCart();
            if (cart?.Items == null || !cart.Items.Any())
                return new List<CartItemViewModel>();

            var productIds = cart.Items.Select(i => i.ProductId).ToList();

            var products = await productRepository.GetProductsByIdsAsync(productIds);

            var productDict = products.ToDictionary(p => p.Id);

            var result = new List<CartItemViewModel>();
            foreach (var entry in cart.Items)
            {
                if (productDict.TryGetValue(entry.ProductId, out var product))
                {
                    var translation = product.Translations.FirstOrDefault(t => t.LangCode == langCode)
                        ?? product.Translations.FirstOrDefault();
                    result.Add(new CartItemViewModel
                    {
                        ProductId = product.Id,
                        Name = translation?.Name ?? "N/A",
                        Price = product.Price,
                        ImageUrl = product.Images.FirstOrDefault()?.ImageUrl ?? "",
                        Quantity = entry.Quantity
                    });
                }
                else
                {
                    cart.Items.Remove(entry);
                }
            }
            SaveCart(cart);
            return result;
        }

        public CartItem UpdateItem(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.Items.Find(i => i.ProductId == productId);

            if(quantity == 0)
            {
                if (item != null) { 
                    cart.Items.Remove(item);
                    SaveCart(cart);
                }
                return new CartItem() { ProductId = productId, Quantity = 0 };
            } 

            if (item == null)
            {
                item = new CartItem()
                {
                    ProductId = productId
                };
                cart.Items.Add(item);
            }
            item.Quantity = quantity;

            SaveCart(cart);
            return item;
        }

        public CartItem GetItem(int productId)
        {
            var cart = GetCart();
            var item = cart.Items.Find(i => i.ProductId == productId);
            item ??= new CartItem()
                {
                    ProductId = productId,
                    Quantity = 0
                };
            return item;
        }
    }
}
