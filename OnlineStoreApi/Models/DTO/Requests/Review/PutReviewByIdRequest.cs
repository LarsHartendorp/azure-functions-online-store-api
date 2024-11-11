namespace OnlineStoreApi.Models.DTO.Requests.Review
{
    public class PutReviewByIdRequest
    {
        public string Content { get; set; } = null!;
        public int Rating { get; set; }
    }
}
