namespace PostService.Domain.Errors;

public class InteractionError
{
    public static Error NotFound =>
        new Error("Interaction.NotFound", "Interaction Not Found!");

    public static Error AlreadyExited =>
        new Error("Interaction.AlreadyExited", "Interaction is Already Exited!");
}
