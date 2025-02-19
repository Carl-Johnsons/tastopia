using RecipeService.Domain.Entities;
namespace RecipeService.Domain.Responses;
public class UserReportRecipeResponse
{
    public UserReportRecipe Report { get; set; } = null!;
    public bool IsRemoved { get; set; }
}
