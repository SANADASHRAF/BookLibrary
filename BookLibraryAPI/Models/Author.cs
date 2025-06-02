using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BookLibraryAPI.Models
{
    public class Author
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
