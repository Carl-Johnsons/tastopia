using Microsoft.AspNetCore.Identity;
namespace IdentityService.Application.Account.Queries;
public class AdminGetNumberOfAccountStatisticQuery : IRequest<Result<List<StatisticEntity>?>>
{
}
public class AdminGetNumberOfAccountStatisticQueryHandler : IRequestHandler<AdminGetNumberOfAccountStatisticQuery, Result<List<StatisticEntity>?>>
{
    private readonly UserManager<ApplicationAccount> _userManager;

    public AdminGetNumberOfAccountStatisticQueryHandler(UserManager<ApplicationAccount> userManager)
    {
        _userManager = userManager;
    }

    public Task<Result<List<StatisticEntity>?>> Handle(AdminGetNumberOfAccountStatisticQuery request, CancellationToken cancellationToken)
    {
        DateTime now = DateTime.UtcNow;
        DateTime startTime;
        startTime = now.AddDays(-366);
        return Task.FromResult(Result<List<StatisticEntity>?>.Success(GetDailyStatistics(startTime, now, "yyyy-MM-dd")));
    }
    private List<StatisticEntity> GetDailyStatistics(DateTime from, DateTime to, string format = "dddd")
    {
        var users = _userManager.Users.AsQueryable();
        var rawData = users
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
