using System.Net;

namespace RecipeService.Domain.Errors;

public class CommentError
{
    public static Error NotFound =>
        new("CommentError.NotFound",
           Message: "Comment not found",
           StatusCode: (int) HttpStatusCode.NotFound);
    public static Error AddCommentFail =>
        new("CommentError.AddCommentFail", 
            Message: "Add Comment fail",
            StatusCode: (int) HttpStatusCode.InternalServerError);
    public static Error DeleteCommentFail =>
        new("CommentError.DeleteCommentFail",
            Message: "Delete Comment fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error UpdateCommentFail =>
            new("CommentError.UpdateCommentFail",
            Message: "Update Comment fail",
            StatusCode: (int)HttpStatusCode.InternalServerError);
    public static Error AlreadyInactive =>
            new("CommentError.AlreadyInactive",
            Message: "Comment already inactive",
            StatusCode: (int)HttpStatusCode.BadRequest);
    public static Error AlreadyActive =>
        new("CommentError.AlreadyActive",
        Message: "Comment already active",
        StatusCode: (int)HttpStatusCode.BadRequest);
}
