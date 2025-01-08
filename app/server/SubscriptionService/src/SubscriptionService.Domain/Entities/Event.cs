using SubscriptionService.Domain.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubscriptionService.Domain.Entities;

public class Event : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [Column(TypeName = "decimal(12,4)")]
    public decimal PriceReduction { get; set; }

    public EventReductionType ReductionType { get; set; }

    public string? Description { get; set; }

    public string? BannerImageUrl { get; set; }
    public bool IsActive { get; set; }
}
