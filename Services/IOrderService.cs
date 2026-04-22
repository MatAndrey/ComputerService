using ComputerService.Models;
using ComputerService.ViewModels;

namespace ComputerService.Services
{
    public interface IOrderService
    {
        Task<decimal> GetOrderSumAsync(CartItem? cartItem);
        Task AddOrderAsync(OrderViewModel viewModel);
        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync(string langCode);
        Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(string langCode);
        Task<OrderViewModel?> GetOrderByIdAsync(string langCode, int id);
    }
}
