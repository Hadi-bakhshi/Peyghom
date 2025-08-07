using MongoDB.Driver;
using Peyghom.Common.Infrastructure.Repository;
using Peyghom.Modules.Chat.Domain;

namespace Peyghom.Modules.Chat.Infrastructure.Repository.Chats;

internal sealed class ChatRepository : BaseRepository<Domain.Chat>, IChatRepository
{
    public ChatRepository(IMongoClient mongoClient, string collectionName) : base(mongoClient, collectionName)
    {
    }

    public Task AddParticipantAsync(string chatId, ChatParticipant participant, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task BlockParticipantAsync(string chatId, string userId, bool isBlocked, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string> CreateAsync(Domain.Chat chat, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(string chatId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Chat?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Domain.Chat>> GetByIdsAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Chat?> GetDirectMessageChatAsync(string userId1, string userId2, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ChatParticipant?> GetParticipantAsync(string chatId, string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<ChatParticipant>> GetParticipantsAsync(string chatId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Domain.Chat>> GetUserChatsAsync(string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserParticipantAsync(string chatId, string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task MuteParticipantAsync(string chatId, string userId, bool isMuted, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveParticipantAsync(string chatId, string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Domain.Chat>> SearchChatsAsync(string userId, string searchTerm, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.Chat chat, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateChatSettingAsync(string chatId, ChatSettings chatSettings, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateLastMessageAsync(string chatId, LastMessageInfo lastMessage, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateParticipantLastReadAsync(string chatId, string userId, string messageId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateParticipantRoleAsync(string chatId, string userId, ParticipantRole role, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected override string GetEntityId(Domain.Chat entity)
    {
        throw new NotImplementedException();
    }
}
