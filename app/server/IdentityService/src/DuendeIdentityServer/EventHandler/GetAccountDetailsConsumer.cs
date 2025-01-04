using Contract.Common;
using Contract.Constants;
using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using IdentityService.Application.Account.Queries;
using MassTransit;

namespace DuendeIdentityServer.EventHandler;

[QueueName(RabbitMQConstant.QUEUE.NAME.GET_ACCOUNT_DETAILS,
    exchangeName: RabbitMQConstant.EXCHANGE.NAME.GET_ACCOUNT_DETAILS,
    type: RabbitMQConstant.EXCHANGE.TYPE.Fanout)]
public class GetAccountDetailsConsumer : IConsumer<GetAccountDetailsEvent>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetAccountDetailsConsumer(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<GetAccountDetailsEvent> context)
    {
        var result = await _sender.Send(new GetAccountDetailQuery
        {
            Ids = new HashSet<Guid> { context.Message.AccountId }
        });
        result.ThrowIfFailure();

        context.Respond(_mapper.Map<AccountDTO>(result.Value![0]));
    }
}
