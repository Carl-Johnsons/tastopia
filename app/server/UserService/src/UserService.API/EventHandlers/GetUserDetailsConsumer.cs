using AutoMapper;
using Contract.Common;
using Contract.Constants;
using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using MassTransit;
using UserService.Application.Users.Commands;

namespace UserService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.GET_USER_DETAILS,
    exchangeName: RabbitMQConstant.EXCHANGE.NAME.GET_USER_DETAILS)]
public class GetUserDetailsConsumer : IConsumer<GetUserDetailsEvent>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetUserDetailsConsumer(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<GetUserDetailsEvent> context)
    {
        var response = await _sender.Send(new GetUserDetailsCommand
        {
            AccountId = context.Message.AccountId,
        });
        response.ThrowIfFailure();

        var user = response.Value;

        var userResponse = _mapper.Map<UserDetailsDTO>(user);

        await context.RespondAsync(userResponse);
    }
}
