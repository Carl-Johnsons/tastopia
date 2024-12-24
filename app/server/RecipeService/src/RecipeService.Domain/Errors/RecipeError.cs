namespace RecipeService.Domain.Errors;

public class RecipeError
{
    public static Error NotFound =>
        new("RecipeError.NotFound",
            "Recipe not found");
    public static Error AddRecipeFail =>
        new("RecipeError.AddRecipeFail", "Add recipe fail");
    public static Error DeleteRecipeFail =>
        new("RecipeError.DeleteRecipeFail", "Delete recipe fail");
    public static Error UpdateRecipeFail =>
            new("RecipeError.UpdateRecipeFail", "Update recipe fail");
}
