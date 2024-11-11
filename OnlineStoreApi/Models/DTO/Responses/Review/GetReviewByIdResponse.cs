namespace OnlineStoreApi.Models.DTO.Responses.Review
{
    public class GetReviewByIdResponse
    {
        public string ReviewId { get; set; } = null!;
        public required string Content { get; set; }
        public int Rating { get; set; }
        public string ProductId { get; set; } = null!;
    }
}
