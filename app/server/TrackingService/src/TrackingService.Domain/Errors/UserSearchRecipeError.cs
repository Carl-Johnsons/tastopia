using System.Net;
namespace TrackingService.Domain.Errors;
public class UserSearchRecipeError
{
    public static Error NotFound =>
        new("UserSearchRecipeError.NotFound",
           Message: "UserSearchRecipeError not found",
           StatusCode: (int)HttpStatusCode.NotFound);
    public static Error AddUserSearchRecipeErrorFail =>
        new("UserSearchRecipeError.AddUserSearchRecipeErrorFail",
            Message: "Add UserSearchRecipeError fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error DeleteUserSearchRecipeErrorFail =>
        new("UserSearchRecipeError.DeleteUserSearchRecipeErrorFail",
            Message: "Delete UserSearchRecipeError fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error UpdateUserSearchRecipeErrorFail =>
            new("UserSearchRecipeError.UpdateUserSearchRecipeErrorFail",
            Message: "Update UserSearchRecipeError fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error NullParameter =>
    new("UserSearchRecipeError.NullParameter",
       Message: "UserSearchRecipeError NullParameter",
       StatusCode: (int)HttpStatusCode.InternalServerError);
}
