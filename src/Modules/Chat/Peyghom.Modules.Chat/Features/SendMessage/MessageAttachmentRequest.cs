namespace Peyghom.Modules.Chat.Features.SendMessage;

public sealed record MessageAttachmentRequest(string FileName,
    long FileSize,
    string FileType,
    string FileUrl,
    string ThumbnailUrl);

