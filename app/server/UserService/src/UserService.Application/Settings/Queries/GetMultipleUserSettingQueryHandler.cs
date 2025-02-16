using DnsClient.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UserService.Domain.Entities;
using UserService.Domain.Errors;

namespace UserService.Application.Settings.Queries;

public record GetMultipleUserSettingQuery : IRequest<Result<Dictionary<Guid, List<UserSetting>>?>>
{
    public HashSet<Guid> AccountIdSet { get; set; } = null!;
}

public class GetMultipleUserSettingQueryHandler : IRequestHandler<GetMultipleUserSettingQuery, Result<Dictionary<Guid, List<UserSetting>>?>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetMultipleUserSettingQueryHandler> _logger;

    public GetMultipleUserSettingQueryHandler(IApplicationDbContext context,
                                              ILogger<GetMultipleUserSettingQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<Dictionary<Guid, List<UserSetting>>?>> Handle(GetMultipleUserSettingQuery request, CancellationToken cancellationToken)
    {
        if (request.AccountIdSet == null)
        {
            return Result<Dictionary<Guid, List<UserSetting>>>.Failure(UserError.NotFound, "Account Id set is null");
        }

        var settings = _context.Settings;
        var settingsDictionary = await settings.ToDictionaryAsync(s => s.Id, s => s);

        var userSettingsDictionary = await _context.UserSettings.Where(us => request.AccountIdSet.Contains(us.AccountId))
                                                                .GroupBy(us => us.AccountId)
                                                                .ToDictionaryAsync(g => g.Key, g => g.ToList());

        foreach (var key in userSettingsDictionary.Keys)
        {
            var userSettings = userSettingsDictionary[key];
            var settingIds = userSettings.Select(usd => usd.SettingId).ToList();

            // populate remaining settings with default value
            foreach (var settingKey in settingsDictionary.Keys)
            {
                if (!settingIds.Contains(settingKey))
                {
                    userSettings.Add(new UserSetting
                    {
                        AccountId = key,
                        SettingId = settingKey,
                        SettingValue = settingsDictionary[settingKey].DefaultValue,
                        Setting = settingsDictionary[settingKey]
                    });
                }
            }
        }

        _logger.LogInformation(JsonConvert.SerializeObject(userSettingsDictionary, Formatting.Indented));

        return Result<Dictionary<Guid, List<UserSetting>>?>.Success(userSettingsDictionary);
    }
}
