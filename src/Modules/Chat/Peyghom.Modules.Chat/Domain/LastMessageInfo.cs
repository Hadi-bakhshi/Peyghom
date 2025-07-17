using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Chat.Domain;

public class LastMessageInfo
{
    [BsonElement("messageId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string MessageId { get; set; }

    [BsonElement("content")] 
    public string Content { get; set; }

    [BsonElement("senderId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string SenderId { get; set; }

    [BsonElement("timestamp")] 
    public DateTime Timestamp { get; set; }

    [BsonElement("messageType")] 
    public MessageType MessageType { get; set; }
}