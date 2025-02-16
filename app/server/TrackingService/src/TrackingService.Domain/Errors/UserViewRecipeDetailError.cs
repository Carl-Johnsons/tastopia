using System.Net;
namespace TrackingService.Domain.Errors;
public class UserViewRecipeDetailError
{
    public static Error NotFound =>
        new("UserViewRecipeDetail.NotFound",
           Message: "UserViewRecipeDetail not found",
           StatusCode: (int)HttpStatusCode.NotFound);
    public static Error AddUserViewRecipeDetailFail =>
        new("UserViewRecipeDetail.AddUserViewRecipeDetailFail",
            Message: "Add UserViewRecipeDetail fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error DeleteUserViewRecipeDetailFail =>
        new("UserViewRecipeDetail.DeleteUserViewRecipeDetailFail",
            Message: "Delete UserViewRecipeDetail fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error UpdateUserViewRecipeDetailFail =>
            new("UserViewRecipeDetail.UpdateUserViewRecipeDetailFail",
            Message: "Update UserViewRecipeDetail fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
}
