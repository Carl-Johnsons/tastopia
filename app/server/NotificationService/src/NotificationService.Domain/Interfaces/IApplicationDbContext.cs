using NotificationService.Domain.Entities;

namespace NotificationService.Domain.Interfaces;

public interface IApplicationDbContext : IDbContext
{
    DbSet<AccountExpoPushToken> AccountExpoPushTokens { get; set; }
}
