using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingService.Domain.Common;

public class BaseAuditableEntityWithoutId
{
    [Column]
    public DateTime CreatedAt { get; set; }
    [Column]
    public DateTime UpdatedAt { get; set; }
}
