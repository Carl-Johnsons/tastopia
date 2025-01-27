using AutoMapper;
using Contract.Common;
using Contract.Constants;
using Contract.DTOs.RecipeDTO;
using Contract.Event.RecipeEvent;
using MassTransit;
using RecipeService.Application.Recipes.Queries;

namespace RecipeService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.GET_RECIPE_DETAILS,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.GET_RECIPE_DETAILS)]
public class GetRecipeDetailsConsumer : IConsumer<GetRecipeDetailsEvent>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetRecipeDetailsConsumer(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<GetRecipeDetailsEvent> context)
    {
        var result = await _sender.Send(new GetRecipeDetailQuery{
            RecipeId = context.Message.RecipeId,
        });
        result.ThrowIfFailure();
        var recipe = _mapper.Map<RecipeDetailsDTO>(result.Value!.Recipe);
        await context.RespondAsync(recipe);
    }
}
