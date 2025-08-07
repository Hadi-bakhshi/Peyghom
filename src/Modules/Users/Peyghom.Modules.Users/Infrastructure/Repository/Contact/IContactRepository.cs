using Peyghom.Common.Application.Repository;
using Peyghom.Modules.Users.Domain;

namespace Peyghom.Modules.Users.Infrastructure.Repository.Contacts;

public interface IContactRepository : IRepository<Contact>
{
    Task<IEnumerable<Contact>> GetUserContactsAsync(string userId);
    Task<IEnumerable<Contact>> GetContactRequestsAsync(string userId);
    Task<IEnumerable<Contact>> GetFavoriteContactsAsync(string userId);
    Task<IEnumerable<Contact>> GetBlockedContactsAsync(string userId);
    Task<Contact?> GetContactAsync(string userId, string contactUserId);
    Task<bool> AreUsersConnectedAsync(string userId1, string userId2);
    Task UpdateStatusAsync(string contactId, ContactStatus status);
    Task BlockContactAsync(string userId, string contactUserId);
    Task UnblockContactAsync(string userId, string contactUserId);
    Task SetFavoriteAsync(string contactId, bool isFavorite);
}
