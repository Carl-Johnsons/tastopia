namespace PostService.Domain.Common;

public sealed record Error(string Code, string? Message = null)
{
    public static readonly Error None = new(string.Empty);

    public static implicit operator Result(Error err) => Result.Failure(err);

}
