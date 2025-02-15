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
    public List<string> ReasonCodes { get; set; } = null!;
    public string? AdditionalDetails { get; set; }
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
            var reasonCodes = request.ReasonCodes;
            var additionalDetails = request.AdditionalDetails;

            if (reporterId == Guid.Empty || recipeId == Guid.Empty || reasonCodes == null || reasonCodes.Count == 0)
            {
                return Result<UserReportRecipeResponse?>.Failure(UserReportRecipeError.NullParameter);
            }
            var recipe = await _context.Recipes.Where(r => r.Id == recipeId).SingleOrDefaultAsync();
            if (recipe == null)
            {
                return Result<UserReportRecipeResponse?>.Failure(UserReportRecipeError.NotFound, "Not found recipe");
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

            var codes = ReportReasonData.RecipeReportReasons.Where(r => reasonCodes.Contains(r.Code)).Select(r => r.Code).ToList();
            if (codes == null || codes.Count == 0) {
                return Result<UserReportRecipeResponse?>.Failure(UserReportRecipeError.NotFound, "Not found reason codes");
            }

            report = new UserReportRecipe
            {
                AccountId = reporterId,
                RecipeId = recipeId,
                ReasonCodes = codes,
                AdditionalDetails = additionalDetails,
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
