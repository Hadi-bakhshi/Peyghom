using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Chat.Domain;

public class ChatSettings
{
    [BsonElement("allowMembersToAddOthers")]
    public bool AllowMembersToAddOthers { get; set; } = true;

    [BsonElement("allowMembersToEditGroupInfo")]
    public bool AllowMembersToEditGroupInfo { get; set; } = false;

    [BsonElement("disappearingMessages")] 
    public bool DisappearingMessages { get; set; } = false;

    [BsonElement("disappearingMessagesDuration")]
    public int DisappearingMessagesDuration { get; set; }
}