using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;
using UserProto;

namespace RecipeService.Application.Recipes.Queries;

public class GetRecipeDetailQuery : IRequest<Result<RecipeDetailsResponse?>>
{
    [Required]
    public Guid RecipeId { get; init; }
}

public class GetRecipeDetailQueryHandler : IRequestHandler<GetRecipeDetailQuery, Result<RecipeDetailsResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public GetRecipeDetailQueryHandler(IApplicationDbContext context,
                        GrpcUser.GrpcUserClient grpcUserClient)
    {
        _context = context;
        _grpcUserClient = grpcUserClient;
    }

    public async Task<Result<RecipeDetailsResponse?>> Handle(GetRecipeDetailQuery request, CancellationToken cancellationToken)
    {
        var recipe = await _context.Recipes
                .SingleOrDefaultAsync(r => r.Id == request.RecipeId);

        if (recipe == null)
        {
            return Result<RecipeDetailsResponse?>.Failure(RecipeError.NotFound);
        }
        recipe.Steps = recipe.Steps.OrderBy(s => s.OrdinalNumber).ToList();
        recipe.Comments = [];
        recipe.RecipeVotes = [];

        var grpcResponse = await _grpcUserClient.GetUserDetailAsync(new GrpcAccountIdRequest
        {
            AccountId = recipe.AuthorId.ToString()
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
            AuthorUsername = grpcResponse.AccountUsername,
            AuthorAvtUrl = grpcResponse.AvatarUrl,
            AuthorDisplayName = grpcResponse.DisplayName,
            AuthorNumberOfFollower = grpcResponse.TotalFollower ?? 0,
            similarRecipes = similarRecipes
        };

        return Result<RecipeDetailsResponse?>.Success(result);
    }
}
