using Contract.Common;
using Contract.Constants;
using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using IdentityService.Application.Account.Queries;
using MassTransit;

namespace DuendeIdentityServer.EventHandler;

[QueueName(RabbitMQConstant.QUEUE.NAME.GET_USER_DETAILS,
    exchangeName: RabbitMQConstant.EXCHANGE.NAME.GET_USER_DETAILS,
    type: RabbitMQConstant.EXCHANGE.TYPE.Fanout)]
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
        var result = await _sender.Send(new GetAccountDetailQuery
        {
            Ids = new HashSet<Guid> { context.Message.AccountId }
        });
        result.ThrowIfFailure();

        context.Respond(_mapper.Map<AccountDTO>(result.Value![0]));
    }
}
