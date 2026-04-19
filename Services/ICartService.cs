using ComputerService.Models;
using ComputerService.ViewModels;

namespace ComputerService.Services
{
    public interface ICartService
    {
        CartItem UpdateItem(int productId, int quantity);
        CartItem GetItem(int productId);
        void ClearCart();
        Task<List<CartItemViewModel>> GetCartWithDetailsAsync(string langCode);
    }
}
