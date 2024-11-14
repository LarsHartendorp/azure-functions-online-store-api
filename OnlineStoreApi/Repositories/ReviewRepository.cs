using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineStoreAPI.Configurations;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IMongoCollection<Review> _reviews;

        public ReviewRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _reviews = database.GetCollection<Review>("Reviews");
        }

        public async Task<IEnumerable<Review>> GetReviewsAsync()
        {
            return await _reviews.Find(_ => true).ToListAsync();
        }

        public async Task<Review?> GetReviewByIdAsync(string id)
        {
            return await _reviews.Find(review => review.ReviewId.ToString() == id).FirstOrDefaultAsync();
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            await _reviews.InsertOneAsync(review);
            return review;
        }

        public async Task<bool> UpdateReview(Review review)
        {
            var result = await _reviews.ReplaceOneAsync(r => r.ReviewId == review.ReviewId, review);
            return result.ModifiedCount > 0;
        }

        public async Task DeleteReviewAsync(string id)
        {
            await _reviews.DeleteOneAsync(review => review.ReviewId.ToString() == id);
        }
        
        public async Task<bool> ReviewExistsAsync(string id)
        {
            var count = await _reviews.CountDocumentsAsync(review => review.ReviewId.ToString() == id);
            return count > 0;
        }
    }
}
