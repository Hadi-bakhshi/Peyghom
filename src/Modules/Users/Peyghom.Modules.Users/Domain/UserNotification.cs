using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Users.Domain;

public class UserNotification
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonElement("type")]
    public NotificationType Type { get; set; }

    [BsonElement("title")]
    public string Title { get; set; }

    [BsonElement("message")]
    public string Message { get; set; }

    [BsonElement("isRead")]
    public bool IsRead { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("readAt")]
    public DateTime? ReadAt { get; set; }

    [BsonElement("relatedEntityId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? RelatedEntityId { get; set; }

    [BsonElement("relatedEntityType")]
    public string? RelatedEntityType { get; set; }
}