namespace NotificationService.Domain.Common;
public class ResultException : Exception
{
    public IEnumerable<Error> Errors { get; }

    public ResultException(IEnumerable<Error> errors)
        : base($"Errors:\n{string.Join("\n", errors.Select(e => $"Code: {e.Code}, Message: {e.Message}"))}")
    {
        Errors = errors;
    }
}
