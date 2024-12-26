using Contract.Common;
using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using MassTransit;
using UserService.Application.Users.Commands;

namespace UserService.API.EventHandlers;

[QueueName("get-user-details-event")]
public class GetUserDetailsConsumer : IConsumer<GetUserDetailsEvent>
{
    private readonly ISender _sender;

    public GetUserDetailsConsumer(ISender sender)
    {
        _sender = sender;
    }
    public async Task Consume(ConsumeContext<GetUserDetailsEvent> context)
    {
        var response = await _sender.Send(new GetUserDetailsCommand
        {
            UserId = context.Message.UserId,
        });
        response.ThrowIfFailure();

        var user = response.Value;

        if(user == null) {
            throw new Exception("Users not found");
        }

        var result = new GetSimpleUsersDTO { 
            Users = mapUser,
        };

        await context.RespondAsync(result);
    }
}
