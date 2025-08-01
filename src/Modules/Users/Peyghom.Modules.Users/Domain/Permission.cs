using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Users.Domain;

public sealed class Permission
{
    public static readonly Permission VerifyUserOtp = new("users:verifyotp");

    public Permission(string code)
    {
        Code = code;
    }

    [BsonElement("code")]
    public string Code { get; }


    public static IEnumerable<Permission> GetAllPermissions()
    {
        return [VerifyUserOtp];
    }
}