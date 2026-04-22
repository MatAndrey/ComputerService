using ComputerService.Models;

namespace ComputerService.Data.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        IQueryable<Order> GetAll();
    }
}
