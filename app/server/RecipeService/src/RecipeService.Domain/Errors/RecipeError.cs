using System.Net;

namespace RecipeService.Domain.Errors;

public class RecipeError
{
    public static Error NotFound =>
        new("RecipeError.NotFound",
           Message: "Recipe not found",
           StatusCode: (int) HttpStatusCode.NotFound);
    public static Error AddRecipeFail =>
        new("RecipeError.AddRecipeFail", 
            Message: "Add recipe fail",
            StatusCode: (int) HttpStatusCode.InternalServerError);
    public static Error DeleteRecipeFail =>
        new("RecipeError.DeleteRecipeFail",
            Message: "Delete recipe fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error UpdateRecipeFail =>
            new("RecipeError.UpdateRecipeFail",
            Message: "Update recipe fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
}
