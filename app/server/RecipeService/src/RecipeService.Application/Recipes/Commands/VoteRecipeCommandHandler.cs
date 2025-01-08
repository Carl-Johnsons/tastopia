using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.Recipes.Commands;

public class VoteRecipeCommand : IRequest<Result>
{
    [Required]
    public bool? IsUpvote { get; init; } = null!;
    [Required]
    public Guid? AccountId { get; init; } = null!;
    [Required]
    public Guid? RecipeId { get; init; } = null!;
}

public class VoteRecipeCommandHandler : IRequestHandler<VoteRecipeCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public VoteRecipeCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(VoteRecipeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var accountId = request.AccountId;
            var recipeId = request.RecipeId;
            var isUpvote = request.IsUpvote;

            if (accountId == null || recipeId == null || isUpvote == null)
            {
                return Result.Failure(RecipeError.NotFound);
            }
            var recipe = await _context.Recipes.Where(r => r.Id == recipeId && r.IsActive == true).FirstOrDefaultAsync();

            if (recipe == null)
            {
                return Result.Failure(RecipeError.NotFound);
            }

            var recipeVote = recipe.RecipeVotes.Where(rv => rv.AccountId == accountId).FirstOrDefault();
            if (recipeVote == null)
            {
                recipeVote = new RecipeVote
                {
                    AccountId = accountId.Value,
                    IsUpvote = isUpvote.Value,
                };
                var delta = isUpvote.Value ? 1 : -1;
                recipe.VoteDiff += delta;
                recipe.RecipeVotes.Add(recipeVote);
                _context.Recipes.Update(recipe);
            }
            else
            {
                var delta = recipeVote.IsUpvote ? isUpvote.Value ? -1 : -2 : isUpvote.Value ? 2 : 1;
                if (recipeVote.IsUpvote == isUpvote.Value)
                {
                    recipe.RecipeVotes.Remove(recipeVote);

                }
                else
                {
                    recipeVote.IsUpvote = isUpvote.Value;
                }
                recipe.VoteDiff += delta;
                _context.Recipes.Update(recipe);
            }
            await _unitOfWork.SaveChangeAsync();
            return Result.Success();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
