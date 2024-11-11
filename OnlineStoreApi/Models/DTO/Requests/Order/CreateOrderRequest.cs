namespace OnlineStoreApi.Models.DTO.Requests.Order
{
    public class CreateOrderRequest
    {
        public string UserId { get; set; }
        public List<string> ProductIds { get; set; } = new List<string>();
    }
}
