using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Users.Domain;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("username")]
    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    [BsonElement("email")]
    [EmailAddress]
    public string? Email { get; set; } 

    [BsonElement("phoneNumber")]
    [Phone]
    public string? PhoneNumber { get; set; }  

    [BsonElement("countryCode")]
    public string? CountryCode { get; set; }
    
    [BsonElement("isEmailVerified")]
    public bool IsEmailVerified { get; set; }

    [BsonElement("isPhoneVerified")]
    public bool IsPhoneVerified { get; set; }

    [BsonElement("passwordHash")]
    [Required]
    public string PasswordHash { get; set; }

    [BsonElement("displayName")]
    public string? DisplayName { get; set; }

    [BsonElement("profilePicture")]
    public string? ProfilePicture { get; set; }

    [BsonElement("isOnline")]
    public bool IsOnline { get; set; }

    [BsonElement("lastSeen")]
    public DateTime? LastSeen { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [BsonElement("status")]
    public UserStatus Status { get; set; }

    [BsonElement("preferences")]
    public UserPreferences? Preferences { get; set; }

    [BsonElement("privacySettings")]
    public UserPrivacySettings? PrivacySettings { get; set; }
}