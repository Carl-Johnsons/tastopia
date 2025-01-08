using SubscriptionService.Domain.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubscriptionService.Domain.Entities;

public class Payment : BaseAuditableEntity
{
    public Guid UserId { get; set; }
    public Guid SettingId { get; set; }
    [Column(TypeName = "decimal(12,4)")]
    public decimal TotalAmount { get; set; }
    [Column(TypeName = "decimal(12,4)")]
    public decimal TotalDiscount { get; set; }
    [Column(TypeName = "decimal(12,4)")]
    public decimal TotalPayment { get; set; }
    public PaymentMethod Method { get; set; }
    public string? Description { get; set; } = null!;
    public PaymentStatus Status { get; set; }

}
