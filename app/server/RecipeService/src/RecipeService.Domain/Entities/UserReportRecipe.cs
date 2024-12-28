using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("UserReportRecipe")]
public class UserReportRecipe : BaseAuditableEntity
{
    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Reason { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Pending";

    public virtual Recipe? Recipe { get; set; }
}
