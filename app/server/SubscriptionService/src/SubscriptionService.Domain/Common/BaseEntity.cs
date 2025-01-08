using System.ComponentModel.DataAnnotations.Schema;

namespace SubscriptionService.Domain.Common;

public class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}
