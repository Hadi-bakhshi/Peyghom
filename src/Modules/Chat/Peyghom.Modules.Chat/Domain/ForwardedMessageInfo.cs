using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Chat.Domain;

public class ForwardedMessageInfo
{
    [BsonElement("originalMessageId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string OriginalMessageId { get; set; }

    [BsonElement("originalSenderId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string OriginalSenderId { get; set; }

    [BsonElement("originalChatId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string OriginalChatId { get; set; }
}