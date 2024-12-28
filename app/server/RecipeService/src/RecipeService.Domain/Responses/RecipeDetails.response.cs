using RecipeService.Domain.Entities;

namespace RecipeService.Domain.Responses;

public class RecipeDetailsResponse
{
    public Recipe Recipe { get; set; } = null!;
    public string AuthorUsername { get; set; } = null!;
    public string AuthorAvtUrl { get; set; } = null!;
    public int AuthorNumberOfFollower { get; set; } = 0;

}
