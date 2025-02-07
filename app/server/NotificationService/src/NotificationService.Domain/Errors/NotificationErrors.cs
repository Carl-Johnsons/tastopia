using System.Net;

namespace NotificationService.Domain.Errors;

public static class NotificationErrors
{
    public static Error NotFound =>
        new("NotificationErrors.NotFound",
            StatusCode: (int)HttpStatusCode.NotFound,
            Message: "Notification not found");
    public static Error ExpoPushTokenNotFound =>
            new("NotificationErrors.ExpoPushTokenNotFound",
                StatusCode: (int)HttpStatusCode.NotFound,
                Message: "Expo push token not found");
}
