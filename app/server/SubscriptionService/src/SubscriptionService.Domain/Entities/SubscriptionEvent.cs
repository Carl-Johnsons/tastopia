namespace SubscriptionService.Domain.Entities;

[PrimaryKey(nameof(SubscriptionId), nameof(EventId))]
public class SubscriptionEvent
{
    public Guid SubscriptionId { get; set; }
    public Guid EventId { get; set; }

    public virtual Subscription Subscription { get; set; } = null!;
    public virtual Event Event { get; set; } = null!;
}
