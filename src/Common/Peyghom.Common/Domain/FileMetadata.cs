using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Common.Domain;

public class FileMetadata
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("originalFileName")]
    public string OriginalFileName { get; set; }

    [BsonElement("storedFileName")]
    public string StoredFileName { get; set; }

    [BsonElement("fileSize")]
    public long FileSize { get; set; }

    [BsonElement("contentType")]
    public string ContentType { get; set; }

    [BsonElement("fileUrl")]
    public string FileUrl { get; set; }

    [BsonElement("uploadedBy")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UploadedBy { get; set; }

    [BsonElement("uploadedAt")]
    public DateTime UploadedAt { get; set; }

    [BsonElement("isDeleted")]
    public bool IsDeleted { get; set; }

    [BsonElement("deleteAfter")]
    public DateTime? DeleteAfter { get; set; }
}