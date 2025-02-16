using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TrackingService.Domain.Entities;
using TrackingService.Domain.Errors;
namespace TrackingService.Application.UserViewRecipeDetails.Commands;

public class CreateUserSearchRecipeCommand : IRequest<Result>
{
    public Guid? AccountId { get; set; }
    public string Keyword { get; set; } = null!;
    public DateTime? SearchTime { get; set; }
}

public class CreateUserSearchRecipeCommandHandler : IRequestHandler<CreateUserSearchRecipeCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateUserSearchRecipeCommandHandler> _logger;

    public CreateUserSearchRecipeCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<CreateUserSearchRecipeCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(CreateUserSearchRecipeCommand request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var searchTime = request.SearchTime;
        var keyword = request.Keyword;

        if(accountId == null || accountId == Guid.Empty || string.IsNullOrEmpty(keyword) || searchTime == null)
        {
            return Result.Failure(UserSearchRecipeError.NullParameter);
        }
        try
        {
            var thresholdDate = DateTime.UtcNow.AddMonths(-2);
            var oldViews = await _context.UserSearchRecipes
                .Where(v => v.AccountId == accountId && v.CreatedAt < thresholdDate)
                .ToListAsync();
            _context.UserSearchRecipes.RemoveRange(oldViews);

            var keytemp = keyword.ToLower();

            var view = await _context.UserSearchRecipes.Where(v => v.Keyword.ToLower() == keytemp).FirstOrDefaultAsync();

            if (view != null)
            {
                view.CreatedAt = searchTime.Value;
                _context.UserSearchRecipes.Update(view);
            }
            else
            {
                view = new UserSearchRecipe
                {
                    AccountId = accountId.Value,
                    CreatedAt = searchTime.Value,
                    Keyword = keyword,
                };
                _context.UserSearchRecipes.Add(view);
            }
            await _unitOfWork.SaveChangeAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(JsonConvert.SerializeObject(ex));
            return Result.Failure(UserSearchRecipeError.AddUserSearchRecipeErrorFail);
        }
    }
}


