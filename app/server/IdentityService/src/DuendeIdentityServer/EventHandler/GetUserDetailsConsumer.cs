using Contract.Common;
using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using IdentityService.Application.Account;
using MassTransit;

namespace DuendeIdentityServer.EventHandler;

[QueueName("get-user-details-event")]
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
        var result = await _sender.Send(new GetAccountDetailCommand
        {
            Id = context.Message.UserId,
        });
        result.ThrowIfFailure();

        context.Respond(_mapper.Map<AccountDTO>(result.Value));
    }
}
