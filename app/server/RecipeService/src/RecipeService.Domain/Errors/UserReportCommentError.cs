using System.Net;

namespace RecipeService.Domain.Errors;

public class UserReportCommentError
{
    public static Error NotFound =>
        new("UserReportCommentError.NotFound",
           Message: "UserReportComment not found",
           StatusCode: (int) HttpStatusCode.NotFound);
    public static Error AddUserReportCommentFail =>
        new("UserReportCommentError.AddUserReportCommentFail", 
            Message: "Add UserReportComment fail",
            StatusCode: (int) HttpStatusCode.InternalServerError);
    public static Error DeleteUserReportCommentFail =>
        new("UserReportCommentError.DeleteUserReportCommentFail",
            Message: "Delete UserReportComment fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error UpdateUserReportCommentFail =>
            new("UserReportCommentError.UpdateUserReportCommentFail",
            Message: "Update UserReportComment fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error NullParameter =>
            new("UserReportCommentError.NullParameter",
            Message: "Null Parameter",
            StatusCode: (int)HttpStatusCode.InternalServerError);

}
