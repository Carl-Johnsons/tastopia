using Microsoft.EntityFrameworkCore;
namespace RecipeService.Application.Recipes.Queries;
public class AdminGetTotalUserNumberQuery : IRequest<Result<int?>>
{
}
public class AdminGetTotalUserNumberQueryHandler : IRequestHandler<AdminGetTotalUserNumberQuery, Result<int?>>
{
    private readonly IApplicationDbContext _context;

    public AdminGetTotalUserNumberQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int?>> Handle(AdminGetTotalUserNumberQuery request, CancellationToken cancellationToken)
    {
        var number = await _context.Users.CountAsync();
        return Result<int?>.Success(number);
    }
}