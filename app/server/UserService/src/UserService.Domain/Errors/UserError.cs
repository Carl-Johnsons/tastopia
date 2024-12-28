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

}
