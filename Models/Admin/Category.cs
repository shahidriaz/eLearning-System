using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace eLearning_System.Models.Admin
{
    [CollectionName("Category")]
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Category")]
        public string CategoryName { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? ParentCategory { get; set; }
    }
}
