using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineStoreAPI.Models
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ReviewId { get; set; }
        public required string Content { get; set; }
        public int Rating { get; set; }
        public required ObjectId ProductId { get; set; } 
    }
}
