using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Users.Domain;

public sealed class Role
{
    public static readonly Role Administrator = new("Administrator");
    public static readonly Role Member = new("Member");
    public static readonly Role Ghost = new("Ghost", [Permission.VerifyUserOtp.Code]);
    
    public Role(string name, List<string>? permissions = null)
    {
        Name = name;
        Permissions = permissions ?? [];
    }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("permissions")]
    public List<string> Permissions { get; set; }
}
