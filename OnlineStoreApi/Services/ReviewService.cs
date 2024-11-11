using MongoDB.Bson;
using OnlineStoreApi.Models.DTO.Requests.Review;
using OnlineStoreApi.Models.DTO.Responses.Review;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.Repositories;

namespace OnlineStoreAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<GetReviewByIdResponse>> GetReviewsWithResponsesAsync()
        {
            var reviews = await _reviewRepository.GetReviewsAsync();
            var responses = reviews.Select(review => new GetReviewByIdResponse
            {
                ReviewId = review.ReviewId.ToString(),
                Content = review.Content,
                Rating = review.Rating,
                ProductId = review.ProductId.ToString()
            });
            return responses;
        }

        public async Task<GetReviewByIdResponse?> GetReviewResponseById(string id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null) return null;

            return new GetReviewByIdResponse
            {
                ReviewId = review.ReviewId.ToString(),
                Content = review.Content,
                Rating = review.Rating,
                ProductId = review.ProductId.ToString()
            };
        }

        public async Task<CreateReviewResponse> AddReviewAsync(CreateReviewRequest request)
        {
            Review review = new Review
            {
                ReviewId = ObjectId.GenerateNewId(),
                Content = request.Content,
                Rating = request.Rating,
                ProductId = ObjectId.Parse(request.ProductId)
            };

            await _reviewRepository.AddReviewAsync(review);

            return new CreateReviewResponse
            {
                Content = review.Content,
                Rating = review.Rating,
            };
        }

        public async Task<bool> UpdateReviewAsync(string id, PutReviewByIdRequest request)
        {
            var existingReview = await _reviewRepository.GetReviewByIdAsync(id);
            if (existingReview == null)
            {
                return false; 
            }

            existingReview.Content = request.Content;
            existingReview.Rating = request.Rating;

            return await _reviewRepository.UpdateReview(existingReview);
        }

        public Task DeleteReviewAsync(string id)
        {
            return _reviewRepository.DeleteReviewAsync(id);
        }

        public Task<bool> ReviewExistsAsync(string id)
        {
            return _reviewRepository.ReviewExistsAsync(id);
        }
    }
}
