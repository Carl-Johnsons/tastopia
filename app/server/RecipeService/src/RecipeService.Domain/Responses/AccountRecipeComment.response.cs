namespace RecipeService.Domain.Responses;

public class AccountRecipeCommentResponse
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string RecipeTitle { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}
