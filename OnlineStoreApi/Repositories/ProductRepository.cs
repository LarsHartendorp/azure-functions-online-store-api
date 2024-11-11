using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineStoreAPI.Configurations;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _products.Find(_ => true).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            return await _products.Find(product => product.ProductId.ToString() == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            var result = await _products.ReplaceOneAsync(p => p.ProductId == product.ProductId, product);
            if (result.ModifiedCount == 0)
            {
                // Optionally handle the case where the product wasn't updated
            }
        }

        public async Task DeleteAsync(string id)
        {
            await _products.DeleteOneAsync(product => product.ProductId.ToString() == id);
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var count = await _products.CountDocumentsAsync(product => product.ProductId.ToString() == id);
            return count > 0;

            // Alternatively, using Find
            // var product = await _products.Find(product => product.ProductId == id).FirstOrDefaultAsync();
            // return product != null;
        }
    }
}
