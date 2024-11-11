namespace OnlineStoreApi.Models.DTO.Requests.Product
{
    public class PutProductByIdRequest
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
