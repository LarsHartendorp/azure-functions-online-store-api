namespace OrderQueueTrigger.Models;

public class Order
{
    public string orderId { get; set; }
    public DateTime orderDate { get; set; }
    public DateTime? shippingDate { get; set; }
    public required string userId { get; set; }
    public List<Product> products { get; set; } = new List<Product>();
}