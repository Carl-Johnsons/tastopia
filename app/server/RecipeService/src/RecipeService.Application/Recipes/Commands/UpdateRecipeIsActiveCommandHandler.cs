using Contract.Event.UserEvent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeService.Domain.Errors;
namespace RecipeService.Application.Recipes.Commands;

public class UpdateRecipeIsActiveCommand : IRequest<Result>
{
    public Guid RecipeId { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateRecipeIsActiveCommandHandler : IRequestHandler<UpdateRecipeIsActiveCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateRecipeIsActiveCommandHandler> _logger;
    private readonly IServiceBus _serviceBus;

    public UpdateRecipeIsActiveCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<UpdateRecipeIsActiveCommandHandler> logger, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(UpdateRecipeIsActiveCommand request, CancellationToken cancellationToken)
    {
        try {
            var recipeId = request.RecipeId;
            var isActive = request.IsActive;
            if (recipeId == Guid.Empty)
            {
                _logger.LogError("RecipeId not found.");
                return Result.Failure(RecipeError.UpdateRecipeFail);
            }

            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);

            if (recipe == null)
            {
                _logger.LogError($"Recipe with id: {recipeId} not found.");
                return Result.Failure(RecipeError.UpdateRecipeFail);
            }
            recipe.IsActive = isActive;
            _context.Recipes.Update(recipe);
            await _unitOfWork.SaveChangeAsync();
            await _serviceBus.Publish(new UpdateUserTotalRecipeEvent
            {
                AccountId = recipe.AuthorId,
                Delta = isActive ? 1 : -1,
            });
            return Result.Success();
        }
        catch (Exception ex) {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result.Failure(RecipeError.UpdateRecipeFail);
        }
    }
}
