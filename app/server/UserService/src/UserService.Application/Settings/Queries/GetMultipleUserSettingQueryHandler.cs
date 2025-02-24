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

    public GetMultipleUserSettingQueryHandler(IApplicationDbContext context)
    {
        _context = context;
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

        foreach (var key in request.AccountIdSet)
        {
            if (userSettingsDictionary.ContainsKey(key))
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
            else
            {
                List<UserSetting> userSettings = [];
                foreach (var settingKey in settingsDictionary.Keys)
                {
                    userSettings.Add(new UserSetting
                    {
                        AccountId = key,
                        SettingId = settingKey,
                        SettingValue = settingsDictionary[settingKey].DefaultValue,
                        Setting = settingsDictionary[settingKey]
                    });
                }
                userSettingsDictionary[key] = userSettings;
            }
        }

        return Result<Dictionary<Guid, List<UserSetting>>?>.Success(userSettingsDictionary);
    }
}
