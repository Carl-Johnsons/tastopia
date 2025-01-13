using RecipeService.Domain.Constants;

namespace RecipeService.Domain.Responses;

public class SimpleRecipeResponse
{

    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string RecipeImgUrl { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string AuthorDisplayName { get; set; } = null!;
    public string AuthorAvtUrl { get; set; } = null!;
    public int VoteDiff { get; set; }
    public int NumberOfComment { get; set; } = 0;
    public Vote Vote { get; set; } = Vote.None;
}
