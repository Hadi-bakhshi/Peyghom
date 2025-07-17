using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Chat.Domain;

public class MessageReaction
{
    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonElement("emoji")] 
    public string Emoji { get; set; }

    [BsonElement("timestamp")] 
    public DateTime Timestamp { get; set; }
}