using ComputerService.Data.Repositories;
using ComputerService.Models;
using ComputerService.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ComputerService.Services
{
    public class OrderService(
        IOrderRepository orderRepository, 
        ICartService cartService, 
        IProductRepository productRepository,
        IHttpContextAccessor httpContextAccessor) : IOrderService
    {
        private readonly ISession Session = httpContextAccessor.HttpContext?.Session ?? throw new Exception("HttpContext not found");
        private readonly string OrdersSessionKey = "Orders";

        public async Task AddOrderAsync(OrderViewModel viewModel)
        {
            Order order = new()
            {
                CustomerName = viewModel.CustomerName,
                Date = DateTime.Now,
                DeliveryAddress = viewModel.DeliveryAddress,
                DeliveryMethod = viewModel.DeliveryMethod,
                Email = viewModel.Email,
                PaymentMethod = viewModel.PaymentMethod,
                Phone = viewModel.Phone,
                Total = 0,
                Status = "new",
                OrderItems = []
            };
            var cart = cartService.GetCart();
            foreach (var cartItem in cart.Items)
            {
                var product = await productRepository.GetProductByIdAsync(cartItem.ProductId);
                if (product == null) continue;
                OrderItem orderItem = new()
                {
                    Order = order,
                    Price = product.Price,
                    Quantity = cartItem.Quantity,
                    Product = product
                };
                order.OrderItems.Add(orderItem);
                order.Total += orderItem.Price * orderItem.Quantity;
            }
            await orderRepository.AddAsync(order);
            AddOrderToSession(order.Id);
            cartService.ClearCart();
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync(string langCode)
        {
            return await GetOrdersAsync(null, langCode);
        }

        public async Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(string langCode)
        {
            var userOrders = GetCurrentUserOrders();
            return await GetOrdersAsync(userOrders, langCode);
        }

        private List<int> GetCurrentUserOrders()
        {
            var value = Session.GetString(OrdersSessionKey);
            return value == null ? [] : JsonSerializer.Deserialize<List<int>>(value) ?? [];
        }

        private void AddOrderToSession(int orderId)
        {
            var orders = GetCurrentUserOrders();
            orders.Add(orderId);
            var json = JsonSerializer.Serialize(orders);
            Session.SetString(OrdersSessionKey, json);
        }

        private async Task<IEnumerable<OrderViewModel>> GetOrdersAsync(List<int>? orderIds, string langCode)
        {
            var orders = orderRepository.GetAll();
            if (orderIds != null)
            {
                orders = orders.Where(o => orderIds.Contains(o.Id));
            }
            return await orders.Select(o => new OrderViewModel
            {
                CustomerName = o.CustomerName,
                Date = o.Date,
                DeliveryAddress = o.DeliveryAddress,
                DeliveryMethod = o.DeliveryMethod,
                Email = o.Email,
                Id = o.Id,
                PaymentMethod = o.PaymentMethod,
                Phone = o.Phone,
                Total = o.Total,
                Items = o.OrderItems.Select(i => new OrderItemViewModel
                {
                    Id = i.Id,
                    OrderId = i.OrderId,
                    Price = i.Price,
                    ProductId = i.ProductId,
                    ProductImage = i.Product.Images.FirstOrDefault().ImageUrl ?? "",
                    ProductName = (
                        i.Product.Translations.FirstOrDefault(t => t.LangCode == langCode) ?? 
                        i.Product.Translations.FirstOrDefault()).Name ?? "N/A",
                    Quantity = i.Quantity
                }).ToList()
            }).ToListAsync();
        }

        public async Task<decimal> GetOrderSumAsync(CartItem? cartItem)
        {
            if(cartItem != null)
            {
                var product = await productRepository.GetProductByIdAsync(cartItem.ProductId);
                if(product != null)
                {
                    return product.Price * cartItem.Quantity;
                }
                return 0m;
            }
            var cart = cartService.GetCart();
            decimal orderSum = 0m;
            foreach(var item in cart.Items)
            {
                var product = await productRepository.GetProductByIdAsync(item.ProductId);
                if (product != null)
                {
                    orderSum += product.Price * item.Quantity;
                }
            }
            return orderSum;
        }

        public async Task<OrderViewModel?> GetOrderByIdAsync(string langCode, int id)
        {
            var orders = orderRepository.GetAll();
            return await orders
                .Where(o => o.Id == id)
                .Select(o => new OrderViewModel
            {
                CustomerName = o.CustomerName,
                Date = o.Date,
                DeliveryAddress = o.DeliveryAddress,
                DeliveryMethod = o.DeliveryMethod,
                Email = o.Email,
                Id = o.Id,
                PaymentMethod = o.PaymentMethod,
                Phone = o.Phone,
                Total = o.Total,
                Items = o.OrderItems.Select(i => new OrderItemViewModel
                {
                    Id = i.Id,
                    OrderId = i.OrderId,
                    Price = i.Price,
                    ProductId = i.ProductId,
                    ProductImage = i.Product.Images.FirstOrDefault().ImageUrl ?? "",
                    ProductName = (
                        i.Product.Translations.FirstOrDefault(t => t.LangCode == langCode) ??
                        i.Product.Translations.FirstOrDefault()).Name ?? "N/A",
                    Quantity = i.Quantity
                }).ToList()
            }).FirstOrDefaultAsync();
        }
    }
}
