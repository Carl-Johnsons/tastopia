using System.Net;

namespace RecipeService.Domain.Errors;

public class ReportError
{
    public static Error NotFound =>
        new("ReportError.NotFound",
           Message: "Report not found",
           StatusCode: (int)HttpStatusCode.NotFound);
    public static Error AlreadyMarkComplete =>
        new("ReportError.AlreadyMarkComplete",
           Message: "Report already mark complete",
           StatusCode: (int)HttpStatusCode.BadRequest);
    public static Error AlreadyPending =>
        new("ReportError.AlreadyPending",
           Message: "Report already pending",
           StatusCode: (int)HttpStatusCode.BadRequest);
    public static Error NullParameter =>
        new("ReportError.NullParameter",
            Message: "Null parameter",
            StatusCode: (int)HttpStatusCode.InternalServerError);
}
