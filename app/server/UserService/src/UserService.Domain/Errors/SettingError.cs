using System.Net;

namespace UserService.Domain.Errors;

public class SettingError
{
    public static Error NotFound =>
        new("SettingError.NotFound",
           StatusCode: (int)HttpStatusCode.NotFound,
           Message: "Setting not found!");

    public static Error InvalidSettingKey =>
        new("SettingError.InvalidSettingKey",
           StatusCode: (int)HttpStatusCode.BadRequest,
           Message: "Invalid setting key");
    public static Error InvalidSettingValue =>
        new("SettingError.InvalidSettingValue",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: "Invalid setting value");
}
