using Contract.Event.TrackingEvent;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;
using UserProto;

namespace RecipeService.Application.Recipes.Queries;

public class GetRecipeDetailQuery : IRequest<Result<RecipeDetailsResponse?>>
{
    [Required]
    public Guid RecipeId { get; init; }

    [Required]
    public Guid AccountId { get; init; }
}

public class GetRecipeDetailQueryHandler : IRequestHandler<GetRecipeDetailQuery, Result<RecipeDetailsResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IServiceBus _serviceBus;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public GetRecipeDetailQueryHandler(IApplicationDbContext context,
                        GrpcUser.GrpcUserClient grpcUserClient,
                        IServiceBus serviceBus)
    {
        _context = context;
        _grpcUserClient = grpcUserClient;
        _serviceBus = serviceBus;
    }

    public async Task<Result<RecipeDetailsResponse?>> Handle(GetRecipeDetailQuery request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var recipeId = request.RecipeId;

        if(accountId == Guid.Empty || recipeId == Guid.Empty)
        {
            return Result<RecipeDetailsResponse?>.Failure(RecipeError.NotFound, "RecipeId or AccountId not found.");
        }

        var recipe = await _context.Recipes
                .SingleOrDefaultAsync(r => r.Id == request.RecipeId && r.IsActive == true);

        if (recipe == null)
        {
            return Result<RecipeDetailsResponse?>.Failure(RecipeError.NotFound);
        }
        recipe.Steps = recipe.Steps.OrderBy(s => s.OrdinalNumber).ToList();

        var grpcResponse = await _grpcUserClient.GetUserDetailAsync(new GrpcAccountIdRequest
        {
            AccountId = recipe.AuthorId.ToString()
        });

        var vote = recipe.RecipeVotes.Where(v => v.AccountId == accountId).SingleOrDefault();
        var v = Vote.None;
        if (vote != null) {
            v = vote.IsUpvote ? Vote.Upvote : Vote.Downvote;
        }
        var bookmark = await _context.UserBookmarkRecipes.Where(bm => bm.AccountId == accountId && bm.RecipeId == recipeId).SingleOrDefaultAsync();

        var isBookmark = false;
        if(bookmark != null) {
            isBookmark = true;
        }

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
            similarRecipes = similarRecipes,
            IsBookmarked = isBookmark,
            Vote = v.ToString(),
            Tags = tagQuery.ToList()
        };

        await _serviceBus.Publish(new CreateUserViewRecipeDetailEvent
        {
            AccountId = request.AccountId,
            RecipeId = request.RecipeId,
            ViewTime = DateTime.UtcNow,
        });

        return Result<RecipeDetailsResponse?>.Success(result);
    }
}
