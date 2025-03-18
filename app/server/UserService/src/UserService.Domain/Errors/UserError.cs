using System.Net;

namespace UserService.Domain.Errors;

public class UserError
{
    public static Error NotFound =>
       new("UserError.NotFound",
           StatusCode: (int)HttpStatusCode.NotFound,
           Message: "User not found!");
    public static Error AlreadyExistUser =>
        new("UserError.AlreadyExistUser",
            Message: "They already have the user, abort adding addition user",
            StatusCode: (int)HttpStatusCode.Conflict);

    public static Error NullParameters =>
        new("UserError.NullParameters",
            Message: "Some parameter is null check the server's log for full errors",
            StatusCode: (int)HttpStatusCode.InternalServerError);

    public static Error FollowFail =>
    new("UserError.FollowFail",
        Message: "User follow fail.",
        StatusCode: (int)HttpStatusCode.InternalServerError);

    public static Error PermissionDenied =>
    new("UserError.PermissionDenied",
        Message: "Permission denied.",
        StatusCode: (int)HttpStatusCode.InternalServerError);

    public static Error UpdateUserFail =>
    new("UserError.UpdateUserFail",
        Message: "Update user fail.",
        StatusCode: (int)HttpStatusCode.InternalServerError);
}
