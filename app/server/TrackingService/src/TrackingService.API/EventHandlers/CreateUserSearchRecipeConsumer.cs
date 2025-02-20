using Contract.Constants;
using Contract.Event.TrackingEvent;
using MassTransit;
using TrackingService.Application.UserSearchRecipes.Commands;
namespace TrackingService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.CREATE_USER_SEARCH_RECIPE,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.CREATE_USER_SEARCH_RECIPE)]
public class CreateUserSearchRecipeConsumer : IConsumer<CreateUserSearchRecipeEvent>
{
    private readonly ISender _sender;

    public CreateUserSearchRecipeConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<CreateUserSearchRecipeEvent> context)
    {
        var result = await _sender.Send(new CreateUserSearchRecipeCommand
        {
            AccountId = context.Message.AccountId,
            Keyword = context.Message.Keyword,
            SearchTime = context.Message.SearchTime,
        });
        result.ThrowIfFailure();
    }
}
