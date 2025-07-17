using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Users.Domain;

public class UserPreferences
{
    [BsonElement("theme")]
    public string Theme { get; set; } = "light";

    [BsonElement("notifications")]
    public bool Notifications { get; set; } = true;

    [BsonElement("soundEnabled")]
    public bool SoundEnabled { get; set; } = true;

    [BsonElement("language")]
    public string Language { get; set; } = "en";
}