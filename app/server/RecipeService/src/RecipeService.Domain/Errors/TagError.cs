using System.Net;

namespace RecipeService.Domain.Errors;

public class TagError
{
    public static Error NotFound =>
        new("TagError.NotFound",
           Message: "Tag not found",
           StatusCode: (int) HttpStatusCode.NotFound);
    public static Error AddTagFail =>
        new("TagError.AddTagFail", Message: "Add Tag fail");
    public static Error DeleteTagFail =>
        new("TagError.DeleteTagFail",Message: "Delete Tag fail");
    public static Error UpdateTagFail =>
            new("TagError.UpdateTagFail",Message: "Update Tag fail");
}
