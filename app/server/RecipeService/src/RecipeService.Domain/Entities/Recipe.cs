using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeService.Domain.Entities;

public class Recipe : BaseAuditableEntity
{
    [Required]
    public Guid AuthorId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength (500)]
    public string Description { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    public List<string> Ingredients { get; set; } = null!;

    public string? CookTime { get; set; }

    public int? Serves { get; set; }
    public int VoteDiff { get; set; } = 0;
    public int NumberOfComment { get; set; } = 0;


    [Required]
    public bool IsActive { get; set; } = true;

    [Required]
    public int TotalView { get; set; } = 0;

    //one to many
    public virtual List<Step>? Steps { get; set; }
    public virtual List<Comment>? Comments { get; set; }
    public virtual List<RecipeVote>? RecipeVotes { get; set; }
    //many to many
    public virtual List<RecipeTag>? RecipeTags { get; set; }


}
