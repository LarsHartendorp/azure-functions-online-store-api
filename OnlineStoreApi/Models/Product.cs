using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OnlineStoreAPI.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ProductId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
