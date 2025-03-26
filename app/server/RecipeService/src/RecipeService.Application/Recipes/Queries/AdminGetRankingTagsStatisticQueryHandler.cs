using MongoDB.Driver;
using RecipeService.Domain.Entities;
namespace RecipeService.Application.Recipes.Queries;
public class AdminGetRankingTagsStatisticQuery : IRequest<Result<List<RankingStatisticEntity>?>>
{
}
public class AdminGetRankingTagsStatisticQueryHandler : IRequestHandler<AdminGetRankingTagsStatisticQuery, Result<List<RankingStatisticEntity>?>>
{
    private readonly IApplicationDbContext _context;

    public AdminGetRankingTagsStatisticQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<RankingStatisticEntity>?>> Handle(AdminGetRankingTagsStatisticQuery request, CancellationToken cancellationToken)
    {
        var alpha = 1.0;
        var beta = 1.0;
        var limit = TAG_CONSTANTS.TAG_RANKING_LIMIT;

        var db = _context.GetDatabase();

        var recipeTagsCollection = db.GetCollection<RecipeTag>(nameof(RecipeTag));
        var recipesCollection = db.GetCollection<Recipe>(nameof(Recipe));
        var tagsCollection = db.GetCollection<Domain.Entities.Tag>(nameof(Domain.Entities.Tag));

        var recipeTagCounts = await recipeTagsCollection.Aggregate()
            .Group(rt => rt.TagId, g => new
            {
                TagId = g.Key,
                RecipeCount = g.Count()
            })
            .ToListAsync(cancellationToken);

        var tagViewCounts = await recipesCollection.Aggregate()
            .Unwind<Recipe>("RecipeTags") 
            .Group(r => r.RecipeTags.Select(rt => rt.TagId), g => new
            {
                TagId = g.Key.FirstOrDefault(),
                TotalView = g.Sum(r => r.TotalView)
            })
            .ToListAsync(cancellationToken);

        var tags = await tagsCollection.Find(_ => true).ToListAsync(cancellationToken);

        var ranking = recipeTagCounts
            .GroupJoin(tagViewCounts,
                rt => rt.TagId,
                tv => tv.TagId,
                (rt, tv) => new
                {
                    rt.TagId,
                    rt.RecipeCount,
                    TotalView = tv.FirstOrDefault()?.TotalView ?? 0
                })
            .Join(tags,
                rt => rt.TagId,
                tag => tag.Id,
                (rt, tag) => new RankingStatisticEntity
                {
                    Title = tag.Value,
                    Number = (int)(alpha * rt.TotalView + beta * rt.RecipeCount)
                })
            .OrderByDescending(t => t.Number)
            .Take(limit)
            .ToList();

        return Result<List<RankingStatisticEntity>?>.Success(ranking);
    }
}
