using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineStoreAPI.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public required ObjectId UserId { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
