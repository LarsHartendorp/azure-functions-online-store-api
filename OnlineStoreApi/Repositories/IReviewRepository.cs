using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsAsync();
        Task<Review?> GetReviewByIdAsync(string id);
        Task<Review> AddReviewAsync(Review review);
        Task<bool> UpdateReview(Review review);
        Task DeleteReviewAsync(string id);
        Task<bool> ReviewExistsAsync(string id);
    }
}
