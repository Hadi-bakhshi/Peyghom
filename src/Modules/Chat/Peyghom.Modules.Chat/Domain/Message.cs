using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Chat.Domain;

public class Message
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }

    [BsonElement("chatId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ChatId { get; set; }

    [BsonElement("senderId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string SenderId { get; set; }

    [BsonElement("content")] 
    public string Content { get; set; }

    [BsonElement("messageType")] 
    public MessageType MessageType { get; set; }

    [BsonElement("timestamp")] 
    public DateTime Timestamp { get; set; }

    [BsonElement("isEdited")] 
    public bool IsEdited { get; set; }

    [BsonElement("editedAt")] 
    public DateTime? EditedAt { get; set; }

    [BsonElement("isDeleted")] 
    public bool IsDeleted { get; set; }

    [BsonElement("deletedAt")] 
    public DateTime? DeletedAt { get; set; }

    [BsonElement("replyToMessageId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ReplyToMessageId { get; set; } 

    [BsonElement("attachments")] 
    public List<MessageAttachment>? Attachments { get; set; }

    [BsonElement("reactions")] 
    public List<MessageReaction>? Reactions { get; set; } 

    [BsonElement("deliveryStatus")] 
    public List<MessageDeliveryStatus>? DeliveryStatus { get; set; } 

    [BsonElement("forwardedFrom")] 
    public ForwardedMessageInfo? ForwardedFrom { get; set; } 
}