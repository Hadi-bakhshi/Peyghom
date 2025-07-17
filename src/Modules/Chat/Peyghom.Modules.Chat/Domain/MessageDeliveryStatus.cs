using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Chat.Domain;

public class MessageDeliveryStatus
{
    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonElement("status")] 
    public DeliveryStatus Status { get; set; }

    [BsonElement("timestamp")] 
    public DateTime Timestamp { get; set; }
}