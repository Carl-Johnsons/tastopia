using Contract.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.Domain.Interfaces;

public interface IApplicationDbContext : IMongoDbContext
{
    DbSet<Notification> Notifications { get; set; }
    DbSet<NotificationTemplate> NotificationTemplates { get; set; }
    DbSet<AccountExpoPushToken> AccountExpoPushTokens { get; set; }
}
