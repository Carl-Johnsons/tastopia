using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;

namespace RecipeService.Application.UserReportRecipes.Commands;
public class UserReportRecipeCommand : IRequest<Result<UserReportRecipeResponse?>>
{
    public Guid ReporterId { get; set; }
    public Guid RecipeId { get; set; }
    public string Reason { get; set; } = null!;
}

public class UserReportRecipeCommandHandler : IRequestHandler<UserReportRecipeCommand, Result<UserReportRecipeResponse?>>
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

    public async Task<Result<UserReportRecipeResponse?>> Handle(UserReportRecipeCommand request, CancellationToken cancellationToken)
    {
        try {
            var reporterId = request.ReporterId;
            var recipeId = request.RecipeId;
            var reason = request.Reason;

            if (reporterId == Guid.Empty || recipeId == Guid.Empty || string.IsNullOrEmpty(reason))
            {
                return Result<UserReportRecipeResponse?>.Failure(UserReportRecipeError.NullParameter);
            }
            var recipe = await _context.Recipes.Where(r => r.Id == recipeId).SingleOrDefaultAsync();
            if (recipe == null)
            {
                return Result<UserReportRecipeResponse?>.Failure(UserReportRecipeError.AddUserReportRecipeFail, "Not found recipe");
            }

            var report = _context.UserReportRecipes.Where(r => r.AccountId == reporterId && r.RecipeId == recipeId).FirstOrDefault();

            if (report != null)
            {
                _context.UserReportRecipes.Remove(report);
                await _unitOfWork.SaveChangeAsync();
                return Result<UserReportRecipeResponse?>.Success(new UserReportRecipeResponse
                {
                    Report = report,
                    IsRemoved = true,
                });
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
            return Result<UserReportRecipeResponse?>.Success(new UserReportRecipeResponse
            {
                Report = report,
                IsRemoved = false,
            });
        }
        catch (Exception ex) {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result<UserReportRecipeResponse?>.Failure(UserReportRecipeError.AddUserReportRecipeFail, ex.Message);
        }
    }
}
