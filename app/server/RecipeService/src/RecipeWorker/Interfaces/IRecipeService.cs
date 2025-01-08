namespace RecipeWorker.Interfaces;

public interface IRecipeService
{
    Task CheckRecipeIngredients();
    Task CheckRecipeTags(Guid recipeId, List<string> tagCodes, List<string> additionTagValues);
    Task CheckRecipeAbuse();
}