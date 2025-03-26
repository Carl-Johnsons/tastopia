using Microsoft.EntityFrameworkCore;

namespace RecipeService.Application.Recipes.Queries;
public class AdminGetRankingRecipesStatisticQuery : IRequest<Result<List<RankingStatisticEntity>?>>
{
}
public class AdminGetRankingRecipesStatisticQueryHandler : IRequestHandler<AdminGetRankingRecipesStatisticQuery, Result<List<RankingStatisticEntity>?>>
{
    private readonly IApplicationDbContext _context;

    public AdminGetRankingRecipesStatisticQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<RankingStatisticEntity>?>> Handle(AdminGetRankingRecipesStatisticQuery request, CancellationToken cancellationToken)
    {
        var limit = RECIPE_CONSTANTS.RANKING_RECIPE_LIMIT;
        var rawData = await _context.Recipes.Where(r => r.TotalView != 0 && r.IsActive).OrderByDescending(r => r.TotalView).Take(limit).Select(r => new RankingStatisticEntity
        {
            Title = r.Title,
            Number = r.TotalView
        }).ToListAsync();

        return Result<List<RankingStatisticEntity>?>.Success(rawData);
    }
}
