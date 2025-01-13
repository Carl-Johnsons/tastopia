using Contract.Common;
using Contract.Constants;
using RecipeWorker.Interfaces;
using MassTransit;
using Contract.Event.RecipeEvent;

namespace RecipeWorker.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.VALIDATE_RECIPE,
    exchangeName: RabbitMQConstant.EXCHANGE.NAME.VALIDATE_RECIPE)]
public class ValidateRecipeConsumer : IConsumer<ValidateRecipeEvent>
{
    private readonly IRecipeService _recipeService;
    private readonly ILogger<ValidateRecipeConsumer> _logger;

    public ValidateRecipeConsumer(IRecipeService recipeService, ILogger<ValidateRecipeConsumer> logger)
    {
        _recipeService = recipeService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ValidateRecipeEvent> context)
    {
        await _recipeService.CheckRecipeTags(context.Message.RecipeId, context.Message.TagValues);

        _logger.LogInformation("Message acknowledged");
    }
}
