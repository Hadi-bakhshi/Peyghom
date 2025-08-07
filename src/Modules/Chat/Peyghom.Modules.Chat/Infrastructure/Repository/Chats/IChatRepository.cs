using Peyghom.Common.Application.Repository;
using Peyghom.Modules.Chat.Domain;

namespace Peyghom.Modules.Chat.Infrastructure.Repository.Chats;

internal interface IChatRepository : IRepository<Domain.Chat>
{
    Task<Domain.Chat?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<Domain.Chat>> GetByIdsAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default);
    Task<string> CreateAsync(Domain.Chat chat, CancellationToken cancellationToken = default);
    Task UpdateAsync(Domain.Chat chat, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    // Chat specific operations
    Task<List<Domain.Chat>> GetUserChatsAsync(string userId, CancellationToken cancellationToken = default);
    Task<Domain.Chat?> GetDirectMessageChatAsync(string userId1, string userId2, CancellationToken cancellationToken = default);
    Task AddParticipantAsync(string chatId, ChatParticipant participant, CancellationToken cancellationToken = default);
    Task RemoveParticipantAsync(string chatId, string userId, CancellationToken cancellationToken = default);
    Task UpdateParticipantRoleAsync(string chatId, string userId, ParticipantRole role, CancellationToken cancellationToken = default);
    Task UpdateParticipantLastReadAsync(string chatId, string userId, string messageId, CancellationToken cancellationToken = default);
    Task UpdateLastMessageAsync(string chatId, LastMessageInfo lastMessage, CancellationToken cancellationToken = default);
    Task<bool> IsUserParticipantAsync(string chatId, string userId, CancellationToken cancellationToken = default);
    Task<ChatParticipant?> GetParticipantAsync(string chatId, string userId, CancellationToken cancellationToken = default);
    Task<List<ChatParticipant>> GetParticipantsAsync(string chatId, CancellationToken cancellationToken = default);
    Task MuteParticipantAsync(string chatId, string userId, bool isMuted, CancellationToken cancellationToken = default);
    Task BlockParticipantAsync(string chatId, string userId, bool isBlocked, CancellationToken cancellationToken = default);
    Task UpdateChatSettingAsync(string chatId, ChatSettings chatSettings, CancellationToken cancellationToken = default);
    Task<List<Domain.Chat>> SearchChatsAsync(string userId, string searchTerm, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string chatId, CancellationToken cancellationToken = default);

}
