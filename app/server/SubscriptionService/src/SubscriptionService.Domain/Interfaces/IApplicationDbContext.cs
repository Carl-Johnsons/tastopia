using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Domain.Interfaces;

public interface IApplicationDbContext : IDbContext
{
    DbSet<Event> Events { get; set; }
    DbSet<Payment> Payments { get; set; }
    DbSet<Subscription> Subscriptions { get; set; }
    DbSet<SubscriptionEvent> SubscriptionEvents { get; set; }
    DbSet<UserSubscription> UserSubscriptions { get; set; }
}
