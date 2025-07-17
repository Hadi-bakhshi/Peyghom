using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Users.Domain;

public class UserPrivacySettings
{
    [BsonElement("allowMessagesFromStrangers")]
    public bool AllowMessagesFromStrangers { get; set; } = true;

    [BsonElement("showOnlineStatus")]
    public bool ShowOnlineStatus { get; set; } = true;

    [BsonElement("showLastSeen")]
    public bool ShowLastSeen { get; set; } = true;

    [BsonElement("allowCalls")]
    public bool AllowCalls { get; set; } = true;
}