using System.Net;

namespace RecipeService.Domain.Errors;

public class TagError
{
    public static Error NotFound =>
        new("TagError.NotFound",
           Message: "Tag not found",
           StatusCode: (int)HttpStatusCode.NotFound);
    public static Error AddTagFail =>
        new("TagError.AddTagFail",
            Message: "Add Tag fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error DeleteTagFail =>
        new("TagError.DeleteTagFail",
            Message: "Delete Tag fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error UpdateTagFail =>
            new("TagError.UpdateTagFail",
            Message: "Update Tag fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error NullParameter =>
            new("TagError.NullParameter",
            Message: "Null Parameter",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error PermissionDeny =>
            new("TagError.PermissionDeny",
            Message: "Permission Deny",
            StatusCode: (int)HttpStatusCode.InternalServerError);

    public static Error AlreadyInactive =>
            new("TagError.AlreadyInactive",
            Message: "Tag already inactive",
            StatusCode: (int)HttpStatusCode.BadRequest);
    public static Error AlreadyExist =>
            new("TagError.AlreadyExist",
            Message: "Tag already exist",
            StatusCode: (int)HttpStatusCode.BadRequest);
    public static Error ExceedLimitDishTypeTag =>
            new("TagError.ExceedLimitDishTypeTag",
            Message: "Exceed limit dish type tag",
            StatusCode: (int)HttpStatusCode.BadRequest);
}
