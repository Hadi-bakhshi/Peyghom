using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Users.Domain;

public class Contact
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonElement("contactUserId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ContactUserId { get; set; }

    [BsonElement("displayName")]
    public string? DisplayName { get; set; }

    [BsonElement("status")]
    public ContactStatus Status { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("isBlocked")]
    public bool IsBlocked { get; set; }

    [BsonElement("isFavorite")]
    public bool IsFavorite { get; set; }
}