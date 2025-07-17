using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Chat.Domain;

public class ChatParticipant
{
    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string UserId { get; set; }

    [BsonElement("role")] 
    public ParticipantRole Role { get; set; }

    [BsonElement("joinedAt")] 
    public DateTime JoinedAt { get; set; }

    [BsonElement("lastReadMessageId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? LastReadMessageId { get; set; }

    [BsonElement("isMuted")] 
    public bool IsMuted { get; set; }

    [BsonElement("isBlocked")] 
    public bool IsBlocked { get; set; }
}