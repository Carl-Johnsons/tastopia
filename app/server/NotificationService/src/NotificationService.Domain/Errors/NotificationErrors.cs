using System.Net;

namespace NotificationService.Domain.Errors;

public static class NotificationErrors
{
    public static Error CategoryNotFound =>
            new("NotificationErrors.CategoryNotFound",
                StatusCode: (int)HttpStatusCode.NotFound,
                Message: "Category not found");
    public static Error ActionNotFound =>
            new("NotificationErrors.ActionNotFound",
                StatusCode: (int)HttpStatusCode.NotFound,
                Message: "Action not found");
}
