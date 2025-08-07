using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Peyghom.Common.Infrastructure.Repository;
using Peyghom.Modules.Chat.Domain;

namespace Peyghom.Modules.Chat.Infrastructure.Repository.Chats;

internal sealed class ChatRepository : BaseRepository<Domain.Chat>, IChatRepository
{
    private readonly ILogger<ChatRepository> _logger;
    public ChatRepository(
        IMongoClient mongoClient,
        ILogger<ChatRepository> logger)
        : base(mongoClient, "chats")
    {
        _logger = logger;
    }

    protected override string GetEntityId(Domain.Chat entity) => entity.Id;

    public override async Task<Domain.Chat?> GetByIdAsync(string id)
    {
        return await FindOneAsync(x => x.Id == id && x.IsActive);
    }

    public override async Task<IEnumerable<Domain.Chat>> GetAllAsync()
    {
        return await FindAsync(x => x.IsActive);
    }

    public override async Task<Domain.Chat> AddAsync(Domain.Chat entity)
    {
        entity.Id = ObjectId.GenerateNewId().ToString();
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.IsActive = true;

        return await base.AddAsync(entity);
    }

    public override async Task<Domain.Chat> UpdateAsync(Domain.Chat entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        return await base.UpdateAsync(entity);
    }

    public override async Task<bool> DeleteAsync(string id)
    {
        var chat = await GetByIdAsync(id);
        if (chat == null) return false;

        chat.IsActive = false;
        chat.UpdatedAt = DateTime.UtcNow;
        await UpdateAsync(chat);
        return true;
    }

    public override async Task<bool> ExistsAsync(string id)
    {
        var count = await CountAsync(x => x.Id == id && x.IsActive);
        return count > 0;
    }

    public async Task AddParticipantAsync(string chatId, ChatParticipant participant, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Chat>.Filter.Eq(x => x.Id, chatId);
        var update = Builders<Domain.Chat>.Update
            .Push(x => x.Participants, participant)
            .Set(x => x.UpdatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task RemoveParticipantAsync(string chatId, string userId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Chat>.Filter.Eq(x => x.Id, chatId);
        var update = Builders<Domain.Chat>.Update
            .PullFilter(x => x.Participants, p => p.UserId == userId)
            .Set(x => x.UpdatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task UpdateParticipantRoleAsync(string chatId, string userId, ParticipantRole role, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Chat>.Filter.And(
            Builders<Domain.Chat>.Filter.Eq(x => x.Id, chatId),
            Builders<Domain.Chat>.Filter.ElemMatch(x => x.Participants, p => p.UserId == userId));

        var update = Builders<Domain.Chat>.Update
            .Set("participants.$.role", role)
            .Set(x => x.UpdatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task UpdateParticipantLastReadAsync(string chatId, string userId, string messageId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Chat>.Filter.And(
                Builders<Domain.Chat>.Filter.Eq(x => x.Id, chatId),
                Builders<Domain.Chat>.Filter.ElemMatch(x => x.Participants, p => p.UserId == userId)
            );

        var update = Builders<Domain.Chat>.Update
                .Set("participants.$.lastReadMessageId", messageId)
                .Set(x => x.UpdatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task UpdateLastMessageAsync(string chatId, LastMessageInfo lastMessage, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Chat>.Filter.Eq(x => x.Id, chatId);
        var update = Builders<Domain.Chat>.Update
                .Set(x => x.LastMessage, lastMessage)
                .Set(x => x.UpdatedAt, DateTime.UtcNow);
        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task<bool> IsUserParticipantAsync(string chatId, string userId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Chat>.Filter.And(
               Builders<Domain.Chat>.Filter.Eq(x => x.Id, chatId),
               Builders<Domain.Chat>.Filter.ElemMatch(x => x.Participants, p => p.UserId == userId),
               Builders<Domain.Chat>.Filter.Eq(x => x.IsActive, true)
           );

        var count = await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        return count > 0;
    }

    public async Task<ChatParticipant?> GetParticipantAsync(string chatId, string userId, CancellationToken cancellationToken = default)
    {
        var chat = await _collection.Find(x => x.Id == chatId && x.IsActive)
            .FirstOrDefaultAsync(cancellationToken);

        return chat?.Participants?.FirstOrDefault(p => p.UserId == userId);
    }

    public async Task<List<ChatParticipant>> GetParticipantsAsync(string chatId, CancellationToken cancellationToken = default)
    {
        var chat = await _collection.Find(x => x.Id == chatId && x.IsActive)
            .Project(x => x.Participants)
            .FirstOrDefaultAsync(cancellationToken);

        return chat ?? [];
    }

    public async Task MuteParticipantAsync(string chatId, string userId, bool isMuted, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Chat>.Filter.And(
                Builders<Domain.Chat>.Filter.Eq(x => x.Id, chatId),
                Builders<Domain.Chat>.Filter.ElemMatch(x => x.Participants, p => p.UserId == userId)
            );

        var update = Builders<Domain.Chat>.Update
            .Set("participants.$.isMuted", isMuted)
            .Set(x => x.UpdatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task BlockParticipantAsync(string chatId, string userId, bool isBlocked, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Chat>.Filter.And(
                Builders<Domain.Chat>.Filter.Eq(x => x.Id, chatId),
                Builders<Domain.Chat>.Filter.ElemMatch(x => x.Participants, p => p.UserId == userId)
            );

        var update = Builders<Domain.Chat>.Update
            .Set("participants.$.isBlocked", isBlocked)
            .Set(x => x.UpdatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task<Domain.Chat?> GetDirectMessageChatAsync(string userId1, string userId2, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Chat>.Filter.And(
            Builders<Domain.Chat>.Filter.Eq(x => x.Type, ChatType.DirectMessage),
            Builders<Domain.Chat>.Filter.Eq(x => x.IsActive, true),
            Builders<Domain.Chat>.Filter.And(
                Builders<Domain.Chat>.Filter.ElemMatch(x => x.Participants, p => p.UserId == userId1),
                Builders<Domain.Chat>.Filter.ElemMatch(x => x.Participants, p => p.UserId == userId2)));

        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Domain.Chat>> GetUserChatsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Chat>.Filter.ElemMatch(
            x => x.Participants, p => p.UserId == userId) & Builders<Domain.Chat>.Filter.Eq(x => x.IsActive, true);

        return await _collection.Find(filter)
            .SortByDescending(x => x.UpdatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Domain.Chat>> SearchChatsAsync(string userId, string searchTerm, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Chat>.Filter.And(
                Builders<Domain.Chat>.Filter.ElemMatch(x => x.Participants, p => p.UserId == userId),
                Builders<Domain.Chat>.Filter.Eq(x => x.IsActive, true),
                Builders<Domain.Chat>.Filter.Or(
                    Builders<Domain.Chat>.Filter.Regex(x => x.Name, new BsonRegularExpression(searchTerm, "i")),
                    Builders<Domain.Chat>.Filter.Regex(x => x.Description, new BsonRegularExpression(searchTerm, "i"))
                )
            );

        return await _collection.Find(filter)
            .SortByDescending(x => x.UpdatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateChatSettingsAsync(string chatId, ChatSettings settings, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Domain.Chat>.Filter.Eq(x => x.Id, chatId);
        var update = Builders<Domain.Chat>.Update
            .Set(x => x.Settings, settings)
            .Set(x => x.UpdatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }
}
