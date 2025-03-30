using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using System.Globalization;
namespace RecipeService.Application.Recipes.Queries;
public class AdminGetNumberOfRecipesStatisticQuery : IRequest<Result<List<StatisticEntity>?>>
{
}
public class AdminGetNumberOfRecipesStatisticQueryHandler : IRequestHandler<AdminGetNumberOfRecipesStatisticQuery, Result<List<StatisticEntity>?>>
{
    private readonly IApplicationDbContext _context;

    public AdminGetNumberOfRecipesStatisticQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result<List<StatisticEntity>?>> Handle(AdminGetNumberOfRecipesStatisticQuery request, CancellationToken cancellationToken)
    {
        DateTime now = DateTime.UtcNow;
        DateTime startTime;
        startTime = now.AddDays(-366);
        return Task.FromResult(Result<List<StatisticEntity>?>.Success(GetDailyStatistics(startTime, now, "yyyy-MM-dd")));
    }
    private List<StatisticEntity> GetDailyStatistics(DateTime from, DateTime to, string format = "dddd")
    {
        var recipes = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable().AsQueryable();
        var rawData = recipes
            .Where(r => r.CreatedAt >= from && r.CreatedAt <= to)
            .GroupBy(r => r.CreatedAt.Date).AsEnumerable()
            .Select(g => new DateStatisticEntity
            {
                Date = g.Key.ToString(format),
                Number = g.Count()
            })
            .ToList();

        return Enumerable.Range(0, (to - from).Days + 1)
            .Select(i =>
            {
                string dateLabel = from.AddDays(i).ToString(format);
                return (StatisticEntity)(rawData.FirstOrDefault(x => x.Date == dateLabel)
                    ?? new DateStatisticEntity { Date = dateLabel, Number = 0 });
            })
            .ToList();
    }
}
