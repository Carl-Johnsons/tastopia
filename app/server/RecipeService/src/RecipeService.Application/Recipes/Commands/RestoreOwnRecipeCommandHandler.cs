using Contract.Event.UserEvent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;

namespace RecipeService.Application.Recipes.Commands;

public class RestoreOwnRecipeCommand : IRequest<Result<Recipe?>>
{
    public Guid RecipeId { get; set; }
    public Guid AuthorId { get; set; }
}

public class RestoreOwnRecipeCommandHandler : IRequestHandler<RestoreOwnRecipeCommand, Result<Recipe?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RestoreOwnRecipeCommandHandler> _logger;
    private readonly IServiceBus _serviceBus;
    public RestoreOwnRecipeCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<RestoreOwnRecipeCommandHandler> logger, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _serviceBus = serviceBus;
    }

    public async Task<Result<Recipe?>> Handle(RestoreOwnRecipeCommand request, CancellationToken cancellationToken)
    {
        try {
            var recipeId = request.RecipeId;
            var authorId = request.AuthorId;
            if (recipeId == Guid.Empty || request.AuthorId == Guid.Empty)
            {
                _logger.LogError("RecipeId or AuthorId not found.");
                return Result<Recipe?>.Failure(RecipeError.NullParameter);
            }

            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);

            if (recipe == null)
            {
                _logger.LogError($"Recipe with id: {recipeId} not found.");
                return Result<Recipe?>.Failure(RecipeError.NotFound);
            }

            if(recipe.AuthorId != authorId)
            {
                _logger.LogError($"Recipe's AuthorId not the same current user AccountId!");
                return Result<Recipe?>.Failure(RecipeError.PermissionDeny, "Recipe's AuthorId not the same current user AccountId!");
            }

            if(recipe.IsActive)
            {
                _logger.LogError($"This recipe not have been deleted, so it cannot be restored.");
                return Result<Recipe?>.Failure(RecipeError.PermissionDeny, "This recipe not have been deleted, so it cannot be restored.");
            }

            var bin = await _context.UserRecipeBins.Where(b => b.AccountId == authorId && b.RecipeId == recipeId).SingleOrDefaultAsync();

            if (bin == null)
            {
                _logger.LogError($"Not found recipe in bin.");
                return Result<Recipe?>.Failure(RecipeError.NotFound, "Not found recipe in bin.");
            }
            recipe.IsActive = true;
            _context.Recipes.Update(recipe);
            _context.UserRecipeBins.Remove(bin);
            await _unitOfWork.SaveChangeAsync();
            recipe.Comments = [];
            recipe.RecipeVotes = [];
            await _serviceBus.Publish(new UpdateUserTotalRecipeEvent
            {
                AccountId = request.AuthorId,
                Delta = 1
            });
            return Result<Recipe?>.Success(recipe);
        }
        catch (Exception ex) {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result<Recipe?>.Failure(RecipeError.UpdateRecipeFail);
        }
    }
}
