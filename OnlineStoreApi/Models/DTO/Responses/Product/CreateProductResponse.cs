namespace OnlineStoreApi.Models.DTO.Responses.Product
{
    public class CreateProductResponse
    {
        public string ProductId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
