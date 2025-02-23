using System.Net;

namespace NotificationService.Domain.Errors;

public static class NotificationErrors
{
    public static Error NotFound =>
        new("NotificationErrors.NotFound",
            StatusCode: (int)HttpStatusCode.NotFound,
            Message: "Notification not found");
    public static Error TemplateNotFound =>
        new("NotificationErrors.TemplateNotFound",
            StatusCode: (int)HttpStatusCode.NotFound,
            Message: "Notification template not found");
    public static Error ExpoPushTokenNotFound =>
            new("NotificationErrors.ExpoPushTokenNotFound",
                StatusCode: (int)HttpStatusCode.NotFound,
                Message: "Expo push token not found");
    public static Error UpdateNotificationFail =>
    new("NotificationErrors.UpdateNotificationFail",
        StatusCode: (int)HttpStatusCode.InternalServerError,
        Message: "Update Notification Fail");
}
