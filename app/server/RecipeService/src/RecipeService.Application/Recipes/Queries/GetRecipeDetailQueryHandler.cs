using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.Recipes.Queries;

public class GetRecipeDetailQuery : IRequest<Result<RecipeDetailsResponse?>>
{
    [Required]
    public Guid RecipeId { get; init; }
}

public class GetRecipeDetailQueryHandler : IRequestHandler<GetRecipeDetailQuery, Result<RecipeDetailsResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IServiceBus _serviceBus;

    public GetRecipeDetailQueryHandler(IApplicationDbContext context, IServiceBus serviceBus)
    {
        _context = context;
        _serviceBus = serviceBus;
    }

    public async Task<Result<RecipeDetailsResponse?>> Handle(GetRecipeDetailQuery request, CancellationToken cancellationToken)
    {
        var recipe = await _context.Recipes
        .FirstOrDefaultAsync(r => r.Id == request.RecipeId);

        if (recipe == null)
        {
            return Result<RecipeDetailsResponse?>.Failure(RecipeError.NotFound);
        }
        recipe.Steps = recipe.Steps.OrderBy(s => s.OrdinalNumber).ToList();
        recipe.Comments = [];
        recipe.RecipeVotes = [];

        var requestClient = _serviceBus.CreateRequestClient<GetUserDetailsEvent>();

        var response = await requestClient.GetResponse<UserDetailsDTO>(new GetUserDetailsEvent
        {
            AccountId = recipe.AuthorId,
        });

        var listString = recipe.Title.ToLower().Split(' ');

        var similarRecipes = _context.Recipes.Where(
            r => r.Id != recipe.Id &&
                (
                 listString.Any(word => r.Title.ToLower().Contains(word)) ||
                 listString.Any(word => r.Description.ToLower().Contains(word)) ||
                 listString.Any(word => r.Ingredients.Any(i => i.ToLower().Contains(word)))
                )
        ).OrderByDescending(r => r.CreatedAt).Select(r => new SimilarRecipe
        {
            ImageUrl = r.ImageUrl,
            RecipeId = r.Id,
            Title = r.Title
        }).Take(6).ToList();



        var result = new RecipeDetailsResponse
        {
            Recipe = recipe,
            AuthorUsername = response.Message.AccountUsername!,
            AuthorAvtUrl = response.Message.AvatarUrl!,
            AuthorDisplayName = response.Message.DisplayName,
            AuthorNumberOfFollower = response.Message.TotalFollwer! ?? 0,
            similarRecipes = similarRecipes
        };

        return Result<RecipeDetailsResponse?>.Success(result);
    }
}
