namespace NotificationService.Domain.Errors;

public static class NotificationErrors
{
    public static Error CategoryNotFound => new("NotificationErrors.CategoryNotFound", "Category not found");
    public static Error ActionNotFound => new("NotificationErrors.ActionNotFound", "Action not found");
}
