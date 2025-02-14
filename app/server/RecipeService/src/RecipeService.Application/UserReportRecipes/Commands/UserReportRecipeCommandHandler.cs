using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;

namespace RecipeService.Application.UserReportRecipes.Commands;

public class UserReportRecipeCommand : IRequest<Result<UserReportRecipe?>>
{
    public Guid ReporterId { get; set; }
    public Guid RecipeId { get; set; }
    public string Reason { get; set; } = null!;
}

public class UserReportRecipeCommandHandler : IRequestHandler<UserReportRecipeCommand, Result<UserReportRecipe?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserReportRecipeCommandHandler> _logger;

    public UserReportRecipeCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<UserReportRecipeCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<UserReportRecipe?>> Handle(UserReportRecipeCommand request, CancellationToken cancellationToken)
    {
        try {
            var reporterId = request.ReporterId;
            var recipeId = request.RecipeId;
            var reason = request.Reason;

            if (reporterId == Guid.Empty || recipeId == Guid.Empty || string.IsNullOrEmpty(reason))
            {
                return Result<UserReportRecipe?>.Failure(UserReportRecipeError.NullParameter);
            }

            var report = _context.UserReportRecipes.Where(r => r.AccountId == reporterId && r.RecipeId == recipeId).FirstOrDefault();

            if (report != null)
            {
                return Result<UserReportRecipe?>.Failure(UserReportRecipeError.AddUserReportRecipeFail, "This report is adready added before.");
            }

            report = new UserReportRecipe
            {
                AccountId = reporterId,
                RecipeId = recipeId,
                Reason = reason,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _context.UserReportRecipes.Add(report);
            await _unitOfWork.SaveChangeAsync();
            return Result<UserReportRecipe?>.Success(report);
        }
        catch (Exception ex) {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result<UserReportRecipe?>.Failure(UserReportRecipeError.AddUserReportRecipeFail, ex.Message);
        }
    }
}
