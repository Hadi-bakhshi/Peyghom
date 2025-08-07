using Peyghom.Common.Application.Repository;
using Peyghom.Modules.Users.Domain;

namespace Peyghom.Modules.Users.Infrastructure.Repository.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);
    Task<User?> FindByEmailAsync(string email);
    Task<User?> FindByPhoneNumberAsync(string phoneNumber);
    Task<IEnumerable<User>> FindByStatusAsync(UserStatus status);
    Task<IEnumerable<User>> FindOnlineUsersAsync();
    Task<IEnumerable<User>> FindByRoleAsync(string roleName);
    Task<bool> IsUsernameAvailableAsync(string username);
    Task<bool> IsEmailAvailableAsync(string email);
    Task UpdateLastSeenAsync(string userId, DateTime lastSeen);
    Task UpdateOnlineStatusAsync(string userId, bool isOnline);
}
