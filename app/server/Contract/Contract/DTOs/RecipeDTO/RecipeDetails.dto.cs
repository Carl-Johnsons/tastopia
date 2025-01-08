
using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.RecipeDTO;

public class RecipeDetailsDTO
{
    [Required]
    public Guid Id { get; set; }
    
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
    public string? CookTime { get; set; }
    public int? Serves { get; set; }
    public int VoteDiff { get; set; } = 0;
    public int NumberOfComment { get; set; } = 0;
    public int TotalView { get; set; } = 0;

    [Required]
    public bool IsActive { get; set; } = true;

    public List<StepDTO> Steps { get; set; } = [];
    public List<CommentDTO> Comments { get; set; } = [];
    public List<RecipeVoteDTO> RecipeVotes { get; set; } = [];
    public virtual List<RecipeTagDTO> RecipeTags { get; set; } = [];
}

public class StepDTO
{
    [Required]
    public int OrdinalNumber { get; set; }
    [Required]
    [MaxLength(500)]
    public string Content { get; set; } = null!;

    public List<string>? AttachedImageUrls { get; set; }
}

public class CommentDTO {
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Content { get; set; } = null!;

    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;
}

public class RecipeVoteDTO
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public bool IsUpvote { get; set; } = true;
}

public class RecipeTagDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid RecipeId { get; set; }
    [Required]
    public Guid TagId { get; set; }
}