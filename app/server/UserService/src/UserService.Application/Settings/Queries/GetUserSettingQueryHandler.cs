using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Application.Settings.Queries;

public record GetUserSettingQuery : IRequest<Result<List<UserSetting>>>
{
    public Guid AccountId { get; set; }
}

public class GetUserSettingQueryHandler : IRequestHandler<GetUserSettingQuery, Result<List<UserSetting>>>
{
    private readonly IApplicationDbContext _context;

    public GetUserSettingQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<UserSetting>>> Handle(GetUserSettingQuery request, CancellationToken cancellationToken)
    {
        var settings = _context.Settings;
        var settingsDictionary = await settings.ToDictionaryAsync(s => s.Id, s => s);

        List<UserSetting> userSettings = await _context.UserSettings.Where(us => us.AccountId == request.AccountId).ToListAsync() ?? [];

        foreach (var userSetting in userSettings)
        {
            settingsDictionary.Remove(userSetting.SettingId);
        }

        // populate remaining setting with default value
        foreach (var key in settingsDictionary.Keys)
        {
            userSettings.Add(new UserSetting
            {
                AccountId = request.AccountId,
                SettingId = key,
                SettingValue = settingsDictionary[key].DefaultValue,
                Setting = settingsDictionary[key]
            });
        }

        return Result<List<UserSetting>>.Success(userSettings);
    }
}
