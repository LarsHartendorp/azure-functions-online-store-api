namespace OnlineStoreApi.Models.DTO.Requests.Review
{
    public class CreateReviewRequest
    {
        public string Content { get; set; } = null!;
        public int Rating { get; set; }
        public string ProductId { get; set; } = null!;
    }
}
