using RecipeService.Domain.Entities;

namespace RecipeService.Domain.Responses;

public class RecipeDetailsResponse
{
    public Recipe Recipe { get; set; } = null!;
    public string AuthorUsername { get; set; } = null!;
    public string AuthorDisplayName { get; set; } = null!;
    public string AuthorAvtUrl { get; set; } = null!;
    public int AuthorNumberOfFollower { get; set; } = 0;
    public List<SimilarRecipe> similarRecipes { get; set; } = [];
}

public class SimilarRecipe
{
    public Guid RecipeId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string Title { get; set; } = null!;

}
