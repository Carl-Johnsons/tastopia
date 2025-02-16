using Contract.Event.TrackingEvent;
using RecipeService.Domain.Errors;

namespace RecipeService.Application.Recipes.Commands;
public class PublishUserSearchRecipeCommand : IRequest<Result<string?>>
{
    public Guid AccountId { get; init; }
    public string Keyword { get; init; } = null!;
}

public class PublishUserSearchRecipeCommandHandler : IRequestHandler<PublishUserSearchRecipeCommand, Result<string?>>
{
    private readonly IServiceBus _serviceBus;

    public PublishUserSearchRecipeCommandHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }
    public async Task<Result<string?>> Handle(PublishUserSearchRecipeCommand request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var keyword = request.Keyword;
        if(accountId == Guid.Empty || string.IsNullOrEmpty(keyword)) {
            return Result<string?>.Failure(RecipeError.NullParameter, "accountId or keyword is null");
        }
        await _serviceBus.Publish(new CreateUserSearchRecipeEvent
        {
            AccountId = accountId,
            Keyword = keyword,
            SearchTime = DateTime.UtcNow,
        });
        return Result<string?>.Success(keyword);
    }
}
