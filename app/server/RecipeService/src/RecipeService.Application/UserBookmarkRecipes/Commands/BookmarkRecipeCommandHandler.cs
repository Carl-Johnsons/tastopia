using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace RecipeService.Application.UserBookmarkRecipes.Commands;

public class BookmarkRecipeCommand : IRequest<Result<BookmarkRecipeResponse?>>
{
    [Required]
    public Guid? AccountId { get; init; } = null!;
    [Required]
    public Guid? RecipeId { get; init; } = null!;
}

public class BookmarkRecipeCommandHandler : IRequestHandler<BookmarkRecipeCommand, Result<BookmarkRecipeResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public BookmarkRecipeCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<BookmarkRecipeResponse?>> Handle(BookmarkRecipeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var accountId = request.AccountId;
            var recipeId = request.RecipeId;

            if (accountId == null || accountId == Guid.Empty || recipeId == null || recipeId == Guid.Empty)
            {
                return Result<BookmarkRecipeResponse?>.Failure(RecipeError.NotFound);
            }
            var recipe = await _context.Recipes.Where(r => r.Id == recipeId && r.IsActive == true).FirstOrDefaultAsync();

            if (recipe == null)
            {
                return Result<BookmarkRecipeResponse?>.Failure(RecipeError.NotFound);
            }

            var bookmark = _context.UserBookmarkRecipes.Where(bm => bm.AccountId == accountId && bm.RecipeId == recipeId).FirstOrDefault();
            var isBookmark = true;
            if (bookmark == null)
            {
                bookmark = new UserBookmarkRecipe
                {
                    Id = Guid.NewGuid(),
                    AccountId = accountId.Value,
                    RecipeId = recipeId.Value,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };
                _context.UserBookmarkRecipes.Add(bookmark);
            }
            else
            {
                isBookmark = false;
                _context.UserBookmarkRecipes.Remove(bookmark);
            }
            await _unitOfWork.SaveChangeAsync();
            bookmark.Recipe = null;
            var result = new BookmarkRecipeResponse
            {
                UserBookmarkRecipe = bookmark,
                IsBookmark = isBookmark
            };
            return Result<BookmarkRecipeResponse?>.Success(result);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
