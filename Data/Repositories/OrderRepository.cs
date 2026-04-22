using ComputerService.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Data.Repositories
{
    public class OrderRepository(AppDbContext dbContext) : IOrderRepository
    {
        public async Task AddAsync(Order order)
        {
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();
        }

        public IQueryable<Order> GetAll()
        {
            return dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product);
        }
    }
}
