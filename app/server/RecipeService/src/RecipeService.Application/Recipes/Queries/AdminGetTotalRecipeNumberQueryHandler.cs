using Microsoft.EntityFrameworkCore;
namespace RecipeService.Application.Recipes.Queries;
public class AdminGetTotalRecipeNumberQuery : IRequest<Result<int?>>
{
}
public class AdminGetTotalRecipeNumberQueryHandler : IRequestHandler<AdminGetTotalRecipeNumberQuery, Result<int?>>
{
    private readonly IApplicationDbContext _context;

    public AdminGetTotalRecipeNumberQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int?>> Handle(AdminGetTotalRecipeNumberQuery request, CancellationToken cancellationToken)
    {
        var number = await _context.Recipes.CountAsync();
        return Result<int?>.Success(number);
    }
}