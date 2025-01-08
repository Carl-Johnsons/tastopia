using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Common;

public class BaseAuditableEntityWithoutId
{
    [Column]
    public DateTime CreatedAt { get; set; }
    [Column]
    public DateTime UpdatedAt { get; set; }
}
