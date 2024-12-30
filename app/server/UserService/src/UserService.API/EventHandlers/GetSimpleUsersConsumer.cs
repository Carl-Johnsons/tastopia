using Contract.Common;
using Contract.Constants;
using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using MassTransit;
using Newtonsoft.Json;
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
        Console.WriteLine(JsonConvert.SerializeObject(context.Message));


        var response = await _sender.Send(new GetSimpleUsersCommand
        {
            AccountIds = context.Message.AccountIds,
        });
        response.ThrowIfFailure();

        var users = response.Value;

        if (users == null || !users.Any())
        {
            throw new Exception("Users not found");
        }

        var mapUser = new Dictionary<Guid, SimpleUser>();

        foreach (var user in users)
        {
            mapUser.Add(user.AccountId, new SimpleUser
            {
                AccountId = user.AccountId,
                AvtUrl = user.AvatarUrl,
                DisplayName = user.DisplayName,
            });
        }

        var result = new GetSimpleUsersDTO
        {
            Users = mapUser,
        };

        await context.RespondAsync(result);
    }
}
