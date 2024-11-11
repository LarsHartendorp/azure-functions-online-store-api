using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OnlineStoreAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId UserId { get; set; }
        public required string Email { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
