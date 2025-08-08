using Mapster;
using Microsoft.Extensions.Logging;
using Peyghom.Common.Application.Messaging;
using Peyghom.Common.Domain;
using Peyghom.Modules.Chat.Domain;
using Peyghom.Modules.Chat.Infrastructure.Repository.Chats;
using Peyghom.Modules.Chat.Infrastructure.Repository.Messages;

namespace Peyghom.Modules.Chat.Features.SendMessage;

public sealed class SendMessageCommandHandler : ICommandHandler<SendMessageCommand, MessageResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly ILogger<SendMessageCommandHandler> _logger;

    public SendMessageCommandHandler(
        IMessageRepository messageRepository,
        IChatRepository chatRepository,
        ILogger<SendMessageCommandHandler> logger)
    {
        _messageRepository = messageRepository;
        _chatRepository = chatRepository;
        _logger = logger;
    }

    public async Task<Result<MessageResponse>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing SendMessage command for ChatId: {ChatId}, SenderId: {SenderId}",
            request.ChatId, request.SenderId);

        // check if sender is in the participant list of the requested chat
        var isUserParticipant =
            await _chatRepository.IsUserParticipantAsync(request.ChatId, request.SenderId, cancellationToken);

        if (!isUserParticipant)
        {
            _logger.LogWarning("User {SenderId} is not a participant in chat {ChatId}", request.SenderId,
                request.ChatId);
            return Result.Failure<MessageResponse>(ChatErrors.UserNotParticipant);
        }

        // the message is replying to another message
        if (!string.IsNullOrEmpty(request.ReplyToMessageId))
        {
            var replyToMessage = await _messageRepository.GetByIdAsync(request.ReplyToMessageId);
            if (replyToMessage is null || replyToMessage.ChatId != request.ChatId)
            {
                _logger.LogWarning("Reply-to message {MessageId} not found or belongs to different chat",
                    request.ReplyToMessageId);

                return Result.Failure<MessageResponse>(MessageErrors.ReplyToMessageNotFound);
            }
        }

        var mapRequestToEntity = request.Adapt<Message>();

        var savedMessage = await _messageRepository.AddAsync(mapRequestToEntity);

        // Update chat's last message
        var lastMessageInfo = new LastMessageInfo
        {
            MessageId = savedMessage.Id,
            Content = savedMessage.Content,
            SenderId = savedMessage.SenderId,
            Timestamp = savedMessage.Timestamp,
            MessageType = savedMessage.MessageType
        };

        await _chatRepository.UpdateLastMessageAsync(request.ChatId, lastMessageInfo, cancellationToken);

        _logger.LogInformation("Message {MessageId} sent successfully in chat {ChatId}", savedMessage.Id,
            request.ChatId);
        
        var mapSavedMessageToResponse = savedMessage.Adapt<MessageResponse>();
        
        return Result.Success(mapSavedMessageToResponse);
    }
}