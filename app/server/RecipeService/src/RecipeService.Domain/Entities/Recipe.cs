using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

public class Recipe : BaseAuditableEntity
{
    [Required]
    public Guid AuthorId { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Column(TypeName = "INTERVAL")]
    public TimeSpan? CookTime { get; set; }
    public int? Serves { get; set; }
    public int? VoteDiff { get; set; }
}
