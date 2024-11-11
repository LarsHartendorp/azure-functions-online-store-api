using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineStoreAPI.Configurations;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _orders = database.GetCollection<Order>("Orders");
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _orders.Find(_ => true).ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(string id)
        {
            return await _orders.Find(order => order.OrderId.ToString() == id).FirstOrDefaultAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orders.InsertOneAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orders.ReplaceOneAsync(o => o.OrderId == order.OrderId, order);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _orders.Find(order => order.UserId.ToString() == userId).ToListAsync();
        }

        public async Task DeleteOrderAsync(string id)
        {
            await _orders.DeleteOneAsync(order => order.OrderId.ToString() == id);
        }

        public async Task<bool> OrderExistsAsync(string id)
        {
            var count = await _orders.CountDocumentsAsync(order => order.OrderId.ToString() == id);
            return count > 0;
        }
    }
}
