using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Common.Domain;

public class DomainEvent1
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("eventType")]
    public string EventType { get; set; }

    [BsonElement("aggregateId")]
    public string AggregateId { get; set; }

    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonElement("data")]
    public object Data { get; set; }

    [BsonElement("timestamp")]
    public DateTime Timestamp { get; set; }

    [BsonElement("processed")]
    public bool Processed { get; set; }

    [BsonElement("processingAttempts")]
    public int ProcessingAttempts { get; set; }
}