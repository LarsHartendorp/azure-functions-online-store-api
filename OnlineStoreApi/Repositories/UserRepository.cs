using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineStoreAPI.Configurations;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _users = database.GetCollection<User>("Users");
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _users.Find(user => user.UserId.ToString() == id).FirstOrDefaultAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(string userId, string email)
        {
            var update = Builders<User>.Update.Set(u => u.Email, email);
            var result = await _users.UpdateOneAsync(u => u.UserId.ToString() == userId, update);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _users.DeleteOneAsync(user => user.UserId.ToString() == id);
        }

        public async Task<bool> UserExistsAsync(string id)
        {
            var count = await _users.CountDocumentsAsync(user => user.UserId.ToString() == id);
            return count > 0;
        }
    }
}
