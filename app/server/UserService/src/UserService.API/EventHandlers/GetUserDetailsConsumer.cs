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

    public GetUserDetailsConsumer(ISender sender)
    {
        _sender = sender;
    }
    public async Task Consume(ConsumeContext<GetUserDetailsEvent> context)
    {
        var response = await _sender.Send(new GetUserDetailsCommand
        {
            AccountId = context.Message.AccountId,
        });
        response.ThrowIfFailure();

        var user = response.Value;

        if (user == null)
        {
            throw new Exception("Users not found");
        }

        var result = new UserDTO
        {
            Address = user.Address,
            AvatarUrl = user.AvatarUrl,
            BackgroundUrl = user.BackgroundUrl,
            Bio = user.Bio,
            DisplayName = user.DisplayName,
            Dob = user.Dob,
            Gender = user.Gender,
            TotalFollowing = user.TotalFollowing ?? 0,
            TotalFollwer = user.TotalFollwer ?? 0,
            TotalRecipe = user.TotalRecipe ?? 0,
        };

        await context.RespondAsync(result);
    }
}
