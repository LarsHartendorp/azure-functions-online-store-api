namespace OrderQueueTrigger.Models;

public class Product
{
    public string productId { get; set; }
    public required string name { get; set; }
    public required string description { get; set; }
    public decimal price { get; set; }
    public string? imageUrl { get; set; }
}