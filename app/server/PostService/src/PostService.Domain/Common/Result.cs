namespace PostService.Domain.Common;

public class Result
{
    protected Result(bool isSuccess, IEnumerable<Error> errors)
    {
        if (isSuccess && errors.Any() || !isSuccess && (errors == null || !errors.Any()))
        {
            throw new InvalidOperationException("Invalid error");
        }
        IsSuccess = isSuccess;
        Errors = errors;
    }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
        {
            throw new InvalidOperationException("Invalid error");
        }
        IsSuccess = isSuccess;
        Errors = [error];
    }

    public bool IsFailure => !IsSuccess;
    public bool IsSuccess { get; }

    public void ThrowIfFailure()
    {
        if (IsFailure && Errors.Any())
        {
            throw new ResultException(Errors);
        }
    }

    public IEnumerable<Error> Errors { get; }
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error err) => new(false, err);
    public static Result Failure(IEnumerable<Error> errs) => new(false, errs);
}

public sealed class Result<T> : Result
{
    private Result(bool isSuccess, Error error, T value) : base(isSuccess, error)
    {
        Value = value;
    }
    private Result(bool isSuccess, IEnumerable<Error> errors, T value) : base(isSuccess, errors)
    {
        Value = value;
    }

    public T Value { get; }

    public static Result<T> Success(T value) => new(true, Error.None, value);
    public static new Result<T?> Failure(Error error) => new(false, error, default);
    public static new Result<T?> Failure(IEnumerable<Error> errs) => new(false, errs, default);
}
