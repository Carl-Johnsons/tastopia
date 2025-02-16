using Contract.Constants;
using Contract.Event.RecipeEvent;
using MassTransit;
using RecipeService.Application.Recipes.Commands;

namespace RecipeService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.UPDATE_RECIPE_IS_ACTIVE,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.UPDATE_RECIPE_IS_ACTIVE)]
public class UpdateRecipeIsActiveConsumer : IConsumer<UpdateRecipeIsActiveEvent>
{
    private readonly ISender _sender;

    public UpdateRecipeIsActiveConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<UpdateRecipeIsActiveEvent> context)
    {
        var result = await _sender.Send(new UpdateRecipeIsActiveCommand
        {
            RecipeId = context.Message.RecipeId,
            IsActive = context.Message.IsActive,
        });
        result.ThrowIfFailure();
    }
}
