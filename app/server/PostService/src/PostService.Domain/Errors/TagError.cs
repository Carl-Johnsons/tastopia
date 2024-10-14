namespace PostService.Domain.Errors;

public class TagError
{
    public static Error NotFound =>
        new ("Tag.NotFound", "Tag Not Found!");

    public static Error AlreadyExited =>
        new ("Tag.AlreadyExited", "Tag is Already Exited!");
}
