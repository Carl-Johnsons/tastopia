namespace PostService.Domain.Errors;

public class ContentRestrictionsError
{
    public static Error TypeNotFound =>
        new Error("ContentRestrictions.TypeNotFound", "Type Not Found!");

    public static Error UserAlreadyInList =>
        new Error("ContentRestrictions.UserAlreadyInList", "User Already In List!");

    public static Error NotFound =>
        new Error("ContentRestrictions.NotFound", "User Content Restrictions Not Found!");

    public static Error MustFromFuture =>
        new Error("ContentRestrictions.MustFromFuture", "Expired time must be from the future!");
}
