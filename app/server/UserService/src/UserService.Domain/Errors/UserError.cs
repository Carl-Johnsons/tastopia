namespace UserService.Domain.Errors;

public class UserError
{
    public static Error NotFound =>
        new("UserError.NotFound",
            "User not found");
    public static Error AlreadyExistUser =>
        new("UserError.AlreadyExistUser",
            "They already have the user, abort adding addition user");

}
