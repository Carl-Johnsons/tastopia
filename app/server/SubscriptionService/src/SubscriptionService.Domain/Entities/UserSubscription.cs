using SubscriptionService.Domain.Constants;

namespace SubscriptionService.Domain.Entities;

public class UserSubscription : BaseAuditableEntity
{
    public Guid PaymentId { get; set; }
    public Guid UserId { get; set; }
    public Guid SubscriptionId { get; set; }
    public SubscriptionStatus Status { get; set; }
    public virtual Payment Payment { get; set; } = null!;
    public virtual Subscription Subscription { get; set; } = null!;
}
