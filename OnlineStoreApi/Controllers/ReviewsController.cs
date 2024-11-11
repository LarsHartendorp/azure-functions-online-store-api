using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.Models.DTO.Requests.Review;
using OnlineStoreApi.Models.DTO.Responses.Review;
using OnlineStoreAPI.Services;

namespace OnlineStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetReviewByIdResponse>>> GetReviews()
        {
            var response = await _reviewService.GetReviewsWithResponsesAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetReviewByIdResponse>> GetReview(string id)
        {
            var response = await _reviewService.GetReviewResponseById(id);
            if (response == null)
            {
                return NotFound(new { message = $"Review with ID {id} not found" });
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CreateReviewResponse>> PostReview(CreateReviewRequest request)
        {
            var response = await _reviewService.AddReviewAsync(request);

            // wat stuur je hier terug?
            //return CreatedAtAction(nameof(GetReview), new { id = response.ReviewId }, response);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PutReviewByIdResponse>> PutReview(string id, PutReviewByIdRequest request)
        {
            var exists = await _reviewService.ReviewExistsAsync(id);
            if (!exists)
            {
                return NotFound(new { message = $"Review with ID {id} not found" });
            }

            await _reviewService.UpdateReviewAsync(id, request);

            var updatedResponse = await _reviewService.GetReviewResponseById(id);
            return Ok(updatedResponse);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(string id)
        {
            var exists = await _reviewService.ReviewExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            await _reviewService.DeleteReviewAsync(id);
            return NoContent();
        }
    }
}
