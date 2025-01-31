using System.Net;

namespace NotificationService.Domain.Errors;

public static class NotificationErrors
{
    public static Error ExpoPushTokenNotFound =>
            new("NotificationErrors.ExpoPushTokenNotFound",
                StatusCode: (int)HttpStatusCode.NotFound,
                Message: "Expo push token not found");
}
