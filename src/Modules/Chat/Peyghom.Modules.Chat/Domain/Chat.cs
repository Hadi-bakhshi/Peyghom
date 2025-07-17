using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Chat.Domain;

public class Chat
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }

    [BsonElement("name")] 
    public string? Name { get; set; } 

    [BsonElement("description")] 
    public string? Description { get; set; } 

    [BsonElement("type")] 
    public ChatType Type { get; set; }

    [BsonElement("participants")] 
    public List<ChatParticipant> Participants { get; set; }

    [BsonElement("createdBy")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CreatedBy { get; set; } 

    [BsonElement("createdAt")] 
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")] 
    public DateTime UpdatedAt { get; set; }

    [BsonElement("lastMessage")] 
    public LastMessageInfo? LastMessage { get; set; } 

    [BsonElement("isActive")] 
    public bool IsActive { get; set; } = true;

    [BsonElement("groupImage")] 
    public string? GroupImage { get; set; } 

    [BsonElement("settings")] 
    public ChatSettings? Settings { get; set; } 
}