using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Domain.Entities;

[Collection("Recipe")]
public class Recipe : BaseMongoDBAuditableEntity
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
    [BsonElement("Steps")]
    public List<Step> Steps { get; set; } = [];

    public List<Comment> Comments { get; set; } = [];

    [BsonElement("RecipeVotes")]
    public List<RecipeVote> RecipeVotes { get; set; } = [];
    //many to many
    public virtual List<RecipeTag> RecipeTags { get; set; } = [];


}

public class Step : BaseMongoDBAuditableEntity
{
    [Required]
    public int OrdinalNumber { get; set; }
    [Required]
    [MaxLength(500)]
    public string Content { get; set; } = null!;

    public List<string>? AttachedImageUrls { get; set; }

}

public class Comment : BaseMongoDBAuditableEntity
{
    [Required]
    [MaxLength(1000)]
    public string Content { get; set; } = null!;

    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;

}
public class RecipeVote : BaseMongoDBAuditableEntity
{
    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public bool IsUpvote { get; set; } = true;
}


