namespace RecipeService.Domain.Responses;

public class RecipeCommentResponse
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    public Guid AccountId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;


}
