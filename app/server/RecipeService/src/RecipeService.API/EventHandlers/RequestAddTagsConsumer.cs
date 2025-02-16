using Contract.Constants;
using Contract.Event.RecipeEvent;
using MassTransit;
using RecipeService.Application.Recipes.Commands;

namespace RecipeService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.REQUEST_ADD_TAGS,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.REQUEST_ADD_TAGS)]
public class RequestAddTagsConsumer : IConsumer<RequestAddTagsEvent>
{
    private readonly ISender _sender;

    public RequestAddTagsConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<RequestAddTagsEvent> context)
    {
        var result = await _sender.Send(new RequestAddTagsCommand
        {
            RecipeId = context.Message.RecipeId,
            Values = context.Message.Requests
        });
        result.ThrowIfFailure();
    }
}
