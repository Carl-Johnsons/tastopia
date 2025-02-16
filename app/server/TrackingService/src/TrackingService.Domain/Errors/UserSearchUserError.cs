using System.Net;
namespace TrackingService.Domain.Errors;

public class UserSearchUserError
{
    public static Error NotFound =>
        new("UserSearchUserError.NotFound",
           Message: "UserSearchUserError not found",
           StatusCode: (int)HttpStatusCode.NotFound);
    public static Error AddUserViewRecipeDetailFail =>
        new("UserSearchUserError.AddUserSearchUserErrorFail",
            Message: "Add UserSearchUserError fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error DeleteUserSearchUserErrorFail =>
        new("UserSearchUserError.DeleteUserSearchUserErrorFail",
            Message: "Delete UserSearchUserError fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error UpdateUserSearchUserErrorFail =>
            new("UserSearchUserError.UpdateUserSearchUserErrorFail",
            Message: "Update UserSearchUserError fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error NullParameter =>
    new("UserSearchUserError.NullParameter",
       Message: "UserSearchUserError NullParameter",
       StatusCode: (int)HttpStatusCode.InternalServerError);
}
