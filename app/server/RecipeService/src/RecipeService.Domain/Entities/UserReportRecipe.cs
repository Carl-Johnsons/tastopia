using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

[Table("UserReportRecipe")]
[PrimaryKey(nameof(UserId), nameof(RecipeId))]
public class UserReportRecipe
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public string Reason { get; set; } = null!;

    [Required]
    public string Status { get; set; } = "Pending";
}
