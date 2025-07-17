using MongoDB.Bson.Serialization.Attributes;

namespace Peyghom.Modules.Chat.Domain;

public class MessageAttachment
{
    [BsonElement("fileName")] 
    public string FileName { get; set; }

    [BsonElement("fileSize")] 
    public long FileSize { get; set; }

    [BsonElement("fileType")] 
    public string FileType { get; set; }

    [BsonElement("fileUrl")] 
    public string FileUrl { get; set; }

    [BsonElement("thumbnailUrl")] 
    public string? ThumbnailUrl { get; set; }

    [BsonElement("uploadedAt")] 
    public DateTime UploadedAt { get; set; }
}