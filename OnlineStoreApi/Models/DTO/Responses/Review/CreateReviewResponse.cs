namespace OnlineStoreApi.Models.DTO.Responses.Review
{
    public class CreateReviewResponse
    {
        public string ReviewId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int Rating { get; set; }
        public string ProductId { get; set; } = null!;
    }
}
