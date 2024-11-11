namespace OnlineStoreApi.Models.DTO.Responses.User

{
    public class GetUserByIdResponse
    {
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<GetUserByIdOrderResponse> Orders { get; set; } = new List<GetUserByIdOrderResponse>();

    }

    public class GetUserByIdOrderResponse
    {
        public string OrderId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public List<GetProductsByIdResponse> Products { get; set; } = new List<GetProductsByIdResponse>();
    }

    public class GetProductsByIdResponse
    {
        public string ProductId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}