using Contract.Common;
using Contract.DTOs.UserDTO;
using Contract.Event.RecipeEvent;
using MassTransit;
using UserService.Application.Users.Commands;

namespace UserService.API.EventHandlers;

[QueueName("get-recipes-event-queue")]
public class GetUsersForRecipesConsumer : IConsumer<GetRecipesEvent>
{
    private readonly ISender _sender;

    public GetUsersForRecipesConsumer(ISender sender)
    {
        _sender = sender;
    }
    public async Task Consume(ConsumeContext<GetRecipesEvent> context)
    {
        var response = await _sender.Send(new GetUsersForRecipesCommand
        {
            UserIds = context.Message.UserIds,
        });
        response.ThrowIfFailure();

        var users = response.Value;

        if(users == null || !users.Any()) {
            throw new Exception("Users not found");
        }

        var mapUser = new Dictionary<Guid ,UserForDisplayRecipe>();

        foreach(var user in users)
        {
            mapUser.Add(user.Id, new UserForDisplayRecipe
            {
                UserId = user.Id,
                AvtUrl = user.AvatarUrl,
                DisplayName = user.DisplayName,
            });
        }

        var result = new GetUsersForDisplayRecipeDTO { 
            Users = mapUser,
        };

        await context.RespondAsync(result);
    }
}
