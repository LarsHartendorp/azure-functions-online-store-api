using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order?> GetOrderByIdAsync(string id);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(string id);
        Task<bool> OrderExistsAsync(string id);
    }

}
