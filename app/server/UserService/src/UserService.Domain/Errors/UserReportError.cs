using System.Net;
namespace UserService.Domain.Errors;
public class UserReportError
{
    public static Error NotFound =>
        new("UserReportError.NotFound",
           Message: "UserReport not found",
           StatusCode: (int)HttpStatusCode.NotFound);
    public static Error AddUserReportFail =>
        new("UserReportError.AddUserReportFail",
            Message: "Add UserReport fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error DeleteUserReportFail =>
        new("UserReportError.DeleteUserReportFail",
            Message: "Delete UserReport fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error UpdateUserReportFail =>
            new("UserReportError.UpdateUserReportFail",
            Message: "Update UserReport fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error NullParameter =>
            new("UserReportError.NullParameter",
            Message: "Null Parameter",
            StatusCode: (int)HttpStatusCode.InternalServerError);

}
