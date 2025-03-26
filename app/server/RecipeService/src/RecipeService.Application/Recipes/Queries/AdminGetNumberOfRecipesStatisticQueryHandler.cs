using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using System.Globalization;
namespace RecipeService.Application.Recipes.Queries;
public class AdminGetNumberOfRecipesStatisticQuery : IRequest<Result<List<StatisticEntity>?>>
{
    public string RangeType { get; set; } = null!;
    public string Language { get; set; } = null!;
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
        var range = request.RangeType;
        var lang = request.Language;
        if (string.IsNullOrEmpty(range) || string.IsNullOrEmpty(lang))
        {
            return Task.FromResult(Result<List<StatisticEntity>?>.Failure(RecipeError.NullParameter, "Language or RangeType is null"));
        }

        DateTime now = DateTime.UtcNow;
        DateTime startTime;

        if (lang != "en" && lang != "vi")
        {
            return Task.FromResult(Result<List<StatisticEntity>?>.Failure(RecipeError.NullParameter, "Invalid language:" + lang));
        }

        switch (range.ToLower())
        {
            case "24h":
                startTime = now.AddHours(-24);
                return Task.FromResult(Result<List<StatisticEntity>?>.Success(GetHourlyStatistics(startTime, now)));
            case "7d":
                startTime = now.AddDays(-7);
                return Task.FromResult(Result<List<StatisticEntity>?>.Success(GetDailyStatistics(startTime, now, lang)));
            case "30d":
                startTime = now.AddDays(-30);
                return Task.FromResult(Result<List<StatisticEntity>?>.Success(GetDailyStatistics(startTime, now, lang, "dd/MM/yyyy")));
            case "3m":
                startTime = now.AddMonths(-3);
                return Task.FromResult(Result<List<StatisticEntity>?>.Success(GetThirtyDaysStatistics(startTime, now, lang)));
            case "12m":
            case "24m":
                int months = int.Parse(range[..^1]);
                startTime = new DateTime(now.Year, now.Month, 1).AddMonths(-months + 1);
                return Task.FromResult(Result<List<StatisticEntity>?>.Success(GetMonthlyStatistics(startTime, now, lang)));
            default:
                return Task.FromResult(Result<List<StatisticEntity>?>.Failure(RecipeError.NullParameter, "Invalid RangeType:" + range));
        }
    }


    private List<StatisticEntity> GetHourlyStatistics(DateTime from, DateTime to)
    {
        var recipes = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable().AsQueryable();
        var rawData = recipes
            .Where(r => r.CreatedAt >= from && r.CreatedAt <= to)
            .GroupBy(r => new { r.CreatedAt.Hour }).AsEnumerable()
            .Select(g => new HourStatisticEntity
            {
                Hour = $"{g.Key.Hour:D2}:00:00",
                Number = g.Count()
            })
            .ToList();

        return Enumerable.Range(0, 24)
            .Select(i =>
            {
                string hourLabel = from.AddHours(i).ToString("HH:mm:ss");
                return (StatisticEntity)(rawData.FirstOrDefault(x => x.Hour == hourLabel)
                    ?? new HourStatisticEntity { Hour = hourLabel, Number = 0 });
            })
            .ToList();
    }

    private List<StatisticEntity> GetDailyStatistics(DateTime from, DateTime to, string lang, string format = "dddd")
    {
        var culture = lang == "vi" ? new CultureInfo("vi-VN") : new CultureInfo("en-US");
        var recipes = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable().AsQueryable();
        var rawData = recipes
            .Where(r => r.CreatedAt >= from && r.CreatedAt <= to)
            .GroupBy(r => r.CreatedAt.Date).AsEnumerable()
            .Select(g => new DateStatisticEntity
            {
                Date = g.Key.ToString(format, culture),
                Number = g.Count()
            })
            .ToList();

        return Enumerable.Range(0, (to - from).Days + 1)
            .Select(i =>
            {
                string dateLabel = from.AddDays(i).ToString(format, culture);
                return (StatisticEntity)(rawData.FirstOrDefault(x => x.Date == dateLabel)
                    ?? new DateStatisticEntity { Date = dateLabel, Number = 0 });
            })
            .ToList();
    }

    private List<StatisticEntity> GetMonthlyStatistics(DateTime from, DateTime to, string lang)
    {
        var monthNames = lang == "vi"
            ? new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
                  "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" }
            : new[] { "January", "February", "March", "April", "May", "June", "July",
                  "August", "September", "October", "November", "December" };
        var recipes = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable().AsQueryable();
        var rawData = recipes
            .Where(r => r.CreatedAt >= from && r.CreatedAt <= to)
            .GroupBy(r => new { r.CreatedAt.Year, r.CreatedAt.Month })
            .Select(g => new MonthStatisticEntity
            {
                Month = monthNames[g.Key.Month - 1],
                Number = g.Count()
            })
            .ToList();

        return Enumerable.Range(0, (to.Year - from.Year) * 12 + to.Month - from.Month + 1)
            .Select(i =>
            {
                var date = from.AddMonths(i);
                string monthLabel = monthNames[date.Month - 1];

                return (StatisticEntity)(rawData.FirstOrDefault(x => x.Month == monthLabel)
                    ?? new MonthStatisticEntity { Month = monthLabel, Number = 0 });
            })
            .ToList();
    }

    private List<StatisticEntity> GetThirtyDaysStatistics(DateTime from, DateTime to, string lang)
    {
        var culture = lang == "vi" ? new CultureInfo("vi-VN") : new CultureInfo("en-US");
        string dateFormat = "dd/MM/yyyy";

        var totalDays = (to - from).Days;
        var step = totalDays / 30; 
        var selectedDays = Enumerable.Range(0, 30)
            .Select(i => from.AddDays(i * step).Date)
            .ToList();

        var recipes = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable().AsQueryable();
        var rawData = recipes
            .Where(r => r.CreatedAt >= from && r.CreatedAt <= to)
            .GroupBy(r => r.CreatedAt.Date).AsEnumerable()
            .Select(g => new DateStatisticEntity
            {
                Date = g.Key.ToString(dateFormat, culture),
                Number = g.Count()
            })
            .ToList();

        return selectedDays
            .Select(day =>
            {
                string dayLabel = day.ToString(dateFormat, culture);
                return (StatisticEntity)(rawData.FirstOrDefault(x => x.Date == dayLabel)
                    ?? new DateStatisticEntity { Date = dayLabel, Number = 0 });
            })
            .ToList();
    }
}
