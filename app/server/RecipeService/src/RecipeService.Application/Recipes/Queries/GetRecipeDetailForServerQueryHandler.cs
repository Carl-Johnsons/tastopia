using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;
using UserProto;

namespace RecipeService.Application.Recipes.Queries;

public class GetRecipeDetailForServerQuery : IRequest<Result<RecipeDetailsResponse?>>
{
    [Required]
    public Guid RecipeId { get; init; }
}

public class GetRecipeDetailForServerQueryHandler : IRequestHandler<GetRecipeDetailForServerQuery, Result<RecipeDetailsResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IServiceBus _serviceBus;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public GetRecipeDetailForServerQueryHandler(IApplicationDbContext context,
                        GrpcUser.GrpcUserClient grpcUserClient,
                        IServiceBus serviceBus)
    {
        _context = context;
        _grpcUserClient = grpcUserClient;
        _serviceBus = serviceBus;
    }

    public async Task<Result<RecipeDetailsResponse?>> Handle(GetRecipeDetailForServerQuery request, CancellationToken cancellationToken)
    {
        var recipeId = request.RecipeId;

        if(recipeId == Guid.Empty)
        {
            return Result<RecipeDetailsResponse?>.Failure(RecipeError.NotFound, "RecipeId not found.");
        }

        var recipe = await _context.Recipes
                .SingleOrDefaultAsync(r => r.Id == request.RecipeId);

        if (recipe == null)
        {
            return Result<RecipeDetailsResponse?>.Failure(RecipeError.NotFound);
        }
        recipe.Steps = recipe.Steps.OrderBy(s => s.OrdinalNumber).ToList();

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

        recipe.RecipeVotes = [];
        recipe.Comments = [];

        var recipeTags = _context.GetDatabase()
            .GetCollection<RecipeTag>(nameof(RecipeTag))
            .AsQueryable();

        var tags = _context.GetDatabase()
            .GetCollection<Domain.Entities.Tag>(nameof(Domain.Entities.Tag))
            .AsQueryable();
        var tagQuery = from rt in recipeTags
                       join t in tags on rt.TagId equals t.Id
                       where rt.RecipeId == recipeId && t.Status == TagStatus.Active
                       select t;
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
