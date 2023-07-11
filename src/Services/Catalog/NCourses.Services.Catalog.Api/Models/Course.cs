using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NCourses.Services.Catalog.Api.Models;

public class Course
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string? Picture { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedTime { get; set; }
    
    public Feature? Feature { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; } = string.Empty;

    [BsonIgnore]
    public Category? Category { get; set; }
}