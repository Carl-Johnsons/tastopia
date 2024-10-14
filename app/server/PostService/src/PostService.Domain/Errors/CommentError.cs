namespace PostService.Domain.Errors;

public class CommentError
{
    public static Error NotFound =>
        new Error("Comment.NotFound", "Comment Not Found!");
    public static Error CantNotDuplicate =>
        new Error("Comment.CantNotDuplicate", "Comment Id Can't Not Duplicate");
}
