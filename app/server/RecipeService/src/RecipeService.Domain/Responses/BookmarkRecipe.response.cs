using RecipeService.Domain.Entities;

namespace RecipeService.Domain.Responses;

public class BookmarkRecipeResponse
{
    public UserBookmarkRecipe UserBookmarkRecipe { get; set; } = null!;
    public bool IsBookmark {  get; set; } 
}
