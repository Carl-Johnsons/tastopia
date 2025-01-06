using SubscriptionService.Domain.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubscriptionService.Domain.Entities;

public class Subscription : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    [Column(TypeName = "interval")]
    public TimeSpan Duration { get; set; }
    public string? Description { get; set; }

    [Column(TypeName = "decimal(12,4)")]
    public decimal Price { get; set; }

    public SubscriptionAccess SubscriptionAccess { get; set; }

    public bool IsActive { get; set; }
}
