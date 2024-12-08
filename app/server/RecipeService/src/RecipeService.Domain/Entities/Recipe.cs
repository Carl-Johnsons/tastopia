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

    [Required]
    public List<string> Ingredients { get; set; } = null!;

    [Column(TypeName = "INTERVAL")]
    public TimeSpan? CookTime { get; set; }
    public int? Serves { get; set; }
    public int? VoteDiff { get; set; }

    //one to many
    public virtual List<Step>? Steps { get; set; }
    public virtual List<Comment>? Comments { get; set; }
    //many to many
    public virtual List<RecipeTag>? RecipeTags { get; set; }
    public virtual List<RecipeVote>? RecipeVotes { get; set; }


}
