using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TrackingService.Domain.Entities;
using TrackingService.Domain.Errors;

namespace TrackingService.Application.UserViewRecipeDetails.Commands;

public class CreateUserVewRecipeDetailCommand : IRequest<Result>
{
    public Guid? AccountId { get; set; }
    public Guid? RecipeId { get; set; }
    public DateTime? ViewTime { get; set; }
}

public class CreateUserVewRecipeDetailCommandHandler : IRequestHandler<CreateUserVewRecipeDetailCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserVewRecipeDetailCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateUserVewRecipeDetailCommand request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var recipeId = request.RecipeId;
        var viewTime = request.ViewTime;

        if(accountId == null || accountId == Guid.Empty || recipeId == null || recipeId == Guid.Empty || viewTime == null)
        {
            return Result.Failure(UserViewRecipeDetailError.NotFound);
        }

        try {
            
            var view = await _context.UserViewRecipeDetails.Where(v => v.RecipeId == recipeId && v.AccountId == accountId).SingleOrDefaultAsync();

            if (view == null) {
                view = new UserViewRecipeDetail
                {
                    Id = Guid.NewGuid(),
                    AccountId = accountId.Value,
                    RecipeId = recipeId.Value,
                    CreatedAt = viewTime.Value,
                    UpdatedAt = viewTime.Value,
                };
                _context.UserViewRecipeDetails.Add(view);
            }
            else
            {
                view.UpdatedAt = viewTime.Value;
                _context.UserViewRecipeDetails.Update(view);
            }
            await _unitOfWork.SaveChangeAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(UserViewRecipeDetailError.AddUserViewRecipeDetailFail);
        }
    }
}


