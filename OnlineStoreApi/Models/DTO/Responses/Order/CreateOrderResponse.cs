namespace OnlineStoreApi.Models.DTO.Responses.Order
{
    public class CreateOrderResponse
    {
        public string OrderId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; } = null!;
        public List<string> ProductIds { get; set; } = new List<string>();
    }
}
