using Contract.Constants;
using Contract.Event.TrackingEvent;
using MassTransit;
using TrackingService.Application.UserSearchUsers.Commands;
namespace TrackingService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.CREATE_USER_SEARCH_USER,
    exchangeName: RabbitMQConstant.EXCHANGE.NAME.CREATE_USER_SEARCH_USER)]
public class CreateUserSearchUserConsumer : IConsumer<CreateUserSearchUserEvent>
{
    private readonly ISender _sender;

    public CreateUserSearchUserConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<CreateUserSearchUserEvent> context)
    {
        var result = await _sender.Send(new CreateUserSearchUserCommand
        {
            AccountId = context.Message.AccountId,
            Keyword = context.Message.Keyword,
            SearchTime = context.Message.SearchTime,
        });
        result.ThrowIfFailure();
    }
}
