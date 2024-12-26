using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.Recipes;

public class GetRecipeDetailCommand : IRequest<Result<RecipeDetailsResponse?>>
{
    [Required]
    public Guid RecipeId { get; init; }
}

public class GetRecipeDetailCommandHandler : IRequestHandler<GetRecipeDetailCommand, Result<RecipeDetailsResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IBus _bus;

    public GetRecipeDetailCommandHandler(IApplicationDbContext context, IBus bus)
    {
        _context = context;
        _bus = bus;
    }

    public async Task<Result<RecipeDetailsResponse?>> Handle(GetRecipeDetailCommand request, CancellationToken cancellationToken)
    {      
        var recipe = await _context.Recipes
        .Include(r => r.Steps!.OrderBy(s=>s.OdinalNumber))
        .FirstOrDefaultAsync(r => r.Id == request.RecipeId);


        if (recipe == null)
        {
            return Result<RecipeDetailsResponse?>.Failure(RecipeError.NotFound);
        }

        var requestClient = _bus.CreateRequestClient<GetUserDetailsEvent>();

        var responseUser = await requestClient.GetResponse<UserDTO>(new GetUserDetailsEvent
        {
            UserId = recipe.AuthorId,
        });


        var responseAccout = await requestClient.GetResponse<AccountDTO>(new GetUserDetailsEvent
        {
            UserId = recipe.AuthorId,
        });

        if(responseAccout == null || responseUser == null) {

            return Result<RecipeDetailsResponse?>.Failure(RecipeError.NotFound);

        }



        var result = new RecipeDetailsResponse
        {
            Recipe = recipe,
            AuthorAvtUrl = responseUser.Message.AvatarUrl!,
            AuthorUsername = responseAccout.Message.Username!,
            AuthorNumberOfFollower = responseUser.Message.TotalFollwer! ?? 0
        };

        return Result<RecipeDetailsResponse?>.Success(result);

    }
}
