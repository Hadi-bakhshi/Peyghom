using Peyghom.Modules.Users.Domain;

namespace Peyghom.Modules.Users.Infrastructure.Repository.UserNotifications;

public interface IUserNotificationRepository : IRepository<UserNotification>
{
    Task<IEnumerable<UserNotification>> GetUserNotificationsAsync(string userId);
    Task<IEnumerable<UserNotification>> GetUnreadNotificationsAsync(string userId);
    Task<long> GetUnreadCountAsync(string userId);
    Task MarkAsReadAsync(string notificationId);
    Task MarkAllAsReadAsync(string userId);
    Task<IEnumerable<UserNotification>> GetNotificationsByTypeAsync(string userId, NotificationType type);
    Task DeleteOldNotificationsAsync(DateTime cutoffDate);
}
