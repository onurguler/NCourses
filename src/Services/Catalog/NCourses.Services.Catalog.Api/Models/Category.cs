using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NCourses.Services.Catalog.Api.Models;

public class Category
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}