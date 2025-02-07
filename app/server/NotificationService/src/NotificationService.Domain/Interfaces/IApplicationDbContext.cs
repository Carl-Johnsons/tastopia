using Contract.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.Domain.Interfaces;

public interface IApplicationDbContext : IMongoDbContext
{
    DbSet<AccountExpoPushToken> AccountExpoPushTokens { get; set; }
}
