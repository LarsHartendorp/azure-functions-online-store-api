using OnlineStoreApi.Models.DTO.Requests.Review;
using OnlineStoreApi.Models.DTO.Responses.Review;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<GetReviewByIdResponse>> GetReviewsWithResponsesAsync();
        Task<GetReviewByIdResponse?> GetReviewResponseById(string id);
        Task<CreateReviewResponse> AddReviewAsync(CreateReviewRequest request);
        Task<bool> UpdateReviewAsync(string id, PutReviewByIdRequest request);
        Task DeleteReviewAsync(string id);
        Task<bool> ReviewExistsAsync(string id);
    }
}
