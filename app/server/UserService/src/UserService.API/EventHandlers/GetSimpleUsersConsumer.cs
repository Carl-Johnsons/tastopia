using Contract.Common;
using Contract.Constants;
using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using MassTransit;
using UserService.Application.Users.Commands;

namespace UserService.API.EventHandlers;

[QueueName(RabbitMQConstant.QUEUE.NAME.GET_SIMPLE_USERS,
    exchangeName: RabbitMQConstant.EXCHANGE.NAME.GET_SIMPLE_USERS)]
public class GetSimpleUsersConsumer : IConsumer<GetSimpleUsersEvent>
{
    private readonly ISender _sender;

    public GetSimpleUsersConsumer(ISender sender)
    {
        _sender = sender;
    }
    public async Task Consume(ConsumeContext<GetSimpleUsersEvent> context)
    {
        var response = await _sender.Send(new GetSimpleUsersCommand
        {
            AccountIds = context.Message.AccountIds,
        });
        response.ThrowIfFailure();

        var users = response.Value!;

        var mapUser = users.Select(u => new SimpleUser
        {
            AccountId = u.AccountId,
            AvtUrl = u.AvatarUrl,
            DisplayName = u.DisplayName,
        }).ToDictionary(u => u.AccountId);

        var result = new GetSimpleUsersDTO
        {
            Users = mapUser,
        };

        await context.RespondAsync(result);
    }
}
