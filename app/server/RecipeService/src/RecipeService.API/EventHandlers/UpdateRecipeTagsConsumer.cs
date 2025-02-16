using Contract.Constants;
using Contract.Event.RecipeEvent;
using MassTransit;
using RecipeService.Application.Recipes.Commands;

namespace RecipeService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.UPDATE_RECIPE_TAGS,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.UPDATE_RECIPE_TAGS)]
public class UpdateRecipeTagsConsumer : IConsumer<UpdateRecipeTagsEvent>
{
    private readonly ISender _sender;

    public UpdateRecipeTagsConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<UpdateRecipeTagsEvent> context)
    {
        var result = await _sender.Send(new UpdateRecipeTagsCommand
        {
            RecipeId = context.Message.RecipeId,
            TagCodes = context.Message.TagCodes,
        });
        result.ThrowIfFailure();
    }
}
