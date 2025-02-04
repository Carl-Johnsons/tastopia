using MassTransit;
using Contract.Event.RecipeEvent;
using Contract.Constants;
using RecipeService.Application.Tags.Queries;
using AutoMapper;
using Contract.DTOs.RecipeDTO;

namespace RecipeService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.GET_ALL_TAGS,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.GET_ALL_TAGS)]
public class GetAllRecipeTagsConsumer : IConsumer<GetAllTagsEvents>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetAllRecipeTagsConsumer(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<GetAllTagsEvents> context)
    {
        var response = await _sender.Send(new GetAllTagsQuery());
        response.ThrowIfFailure();
        var result = new TagListDTO
        {
            Tags = []
        };
        foreach(var t in response.Value)
        {
            result.Tags.Add(_mapper.Map<TagDTO>(t));
        }
        await context.RespondAsync(result);
    }
}
