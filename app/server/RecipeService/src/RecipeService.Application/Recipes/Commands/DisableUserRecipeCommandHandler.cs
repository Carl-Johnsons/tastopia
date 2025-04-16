using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;

namespace RecipeService.Application.Recipes.Commands;

public class DisableUserRecipeCommand : IRequest<Result>
{
    public Guid AccountId { get; set; }
}

public class DisableUserRecipeCommandHandler : IRequestHandler<DisableUserRecipeCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public DisableUserRecipeCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DisableUserRecipeCommand request, CancellationToken cancellationToken)
    {
        try {
            var accountId = request.AccountId;
            if (accountId == Guid.Empty)
            {
                return Result.Failure(RecipeError.NullParameter, "Not found account id.");
            }

            var recipes = await _context.Recipes
                .Where(r => 
                    r.AuthorId == accountId || 
                    r.Comments.Any(c => c.AccountId == accountId)
                ).ToListAsync();

            foreach(var recipe in recipes)
            {
                if(recipe.AuthorId == accountId && recipe.IsActive == true)
                {
                    recipe.IsActive = false;
                }
                    await Console.Out.WriteLineAsync("update recipe:" + recipe.Title);
                foreach(var comment in recipe.Comments)
                {
                    if(comment.AccountId == accountId)
                    {
                        await Console.Out.WriteLineAsync("           update recipe comment:" + comment.AccountId);
                        comment.IsActive = false;
                    }
                }

            }
            _context.Recipes.UpdateRange(recipes);
            await _unitOfWork.SaveChangeAsync();
            return Result.Success();
        }
        catch (Exception ex) {
            return Result<Recipe?>.Failure(RecipeError.UpdateRecipeFail, ex.Message);
        }
    }
}
