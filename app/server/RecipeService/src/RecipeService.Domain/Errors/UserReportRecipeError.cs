using System.Net;

namespace RecipeService.Domain.Errors;

public class UserReportRecipeError
{
    public static Error NotFound =>
        new("UserReportRecipeError.NotFound",
           Message: "UserReportRecipe not found",
           StatusCode: (int) HttpStatusCode.NotFound);
    public static Error AddUserReportRecipeFail =>
        new("UserReportRecipeError.AddUserReportRecipeFail", 
            Message: "Add UserReportRecipe fail",
            StatusCode: (int) HttpStatusCode.InternalServerError);
    public static Error DeleteUserReportRecipeFail =>
        new("UserReportRecipeError.DeleteUserReportRecipeFail",
            Message: "Delete UserReportRecipe fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error UpdateUserReportRecipeFail =>
            new("UserReportRecipeError.UpdateUserReportRecipeFail",
            Message: "Update UserReportRecipe fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error NullParameter =>
            new("UserReportRecipeError.NullParameter",
            Message: "Null Parameter",
            StatusCode: (int)HttpStatusCode.InternalServerError);

}
