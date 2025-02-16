using Contract.Event.TrackingEvent;
using UserService.Domain.Errors;

namespace RecipeService.Application.Recipes.Commands;
public class PublishUserSearchUserCommand : IRequest<Result<string?>>
{
    public Guid AccountId { get; init; }
    public string Keyword { get; init; } = null!;
}

public class PublishUserSearchUserCommandHandler : IRequestHandler<PublishUserSearchUserCommand, Result<string?>>
{
    private readonly IServiceBus _serviceBus;

    public PublishUserSearchUserCommandHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }
    public async Task<Result<string?>> Handle(PublishUserSearchUserCommand request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var keyword = request.Keyword;
        if(accountId == Guid.Empty || string.IsNullOrEmpty(keyword)) {
            return Result<string?>.Failure(UserError.NullParameters, "accountId or keyword is null");
        }
        await _serviceBus.Publish(new CreateUserSearchUserEvent
        {
            AccountId = accountId,
            Keyword = keyword,
            SearchTime = DateTime.UtcNow,
        });
        return Result<string?>.Success(keyword);
    }
}
