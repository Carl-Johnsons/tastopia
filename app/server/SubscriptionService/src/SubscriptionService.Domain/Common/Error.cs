namespace SubscriptionService.Domain.Common;
public sealed record Error(string Code, int? StatusCode = null, string? Message = null)
{
    public static readonly Error None = new(string.Empty);

    public static implicit operator Result(Error err) => Result.Failure(err);

}
