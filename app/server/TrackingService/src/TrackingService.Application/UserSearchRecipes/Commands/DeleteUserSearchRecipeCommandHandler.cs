using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TrackingService.Domain.Entities;
using TrackingService.Domain.Errors;
namespace TrackingService.Application.UserViewRecipeDetails.Commands;

public class DeleteUserSearchRecipeCommand : IRequest<Result<UserSearchRecipe?>>
{
    public Guid? AccountId { get; set; }
    public string Keyword { get; set; } = null!;
}

public class DeleteUserSearchRecipeCommandHandler : IRequestHandler<DeleteUserSearchRecipeCommand, Result<UserSearchRecipe?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteUserSearchRecipeCommandHandler> _logger;

    public DeleteUserSearchRecipeCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<DeleteUserSearchRecipeCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<UserSearchRecipe?>> Handle(DeleteUserSearchRecipeCommand request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var keyword = request.Keyword;

        if(accountId == null || accountId == Guid.Empty || string.IsNullOrEmpty(keyword))
        {
            return Result<UserSearchRecipe?>.Failure(UserSearchRecipeError.NullParameter, "AccountId or Keyword is null.");
        }
        try
        {
            keyword = keyword.ToLower();
            var view = await _context.UserSearchRecipes.Where(v => v.Keyword.ToLower() == keyword).FirstOrDefaultAsync();
            if (view == null)
            {
                return Result<UserSearchRecipe?>.Failure(UserSearchRecipeError.NotFound, "Not found recipe keyword to remove.");
            }
            _context.UserSearchRecipes.Remove(view);
            await _unitOfWork.SaveChangeAsync();
            return Result<UserSearchRecipe?>.Success(view);
        }
        catch (Exception ex)
        {
            _logger.LogError(JsonConvert.SerializeObject(ex));
            return Result<UserSearchRecipe?>.Failure(UserSearchRecipeError.DeleteUserSearchRecipeErrorFail);
        }
    }
}


