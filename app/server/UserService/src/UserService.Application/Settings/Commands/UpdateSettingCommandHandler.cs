using Contract.Constants;
using UserService.Domain.Entities;
using UserService.Domain.Errors;

namespace UserService.Application.Settings.Commands;

public record SettingObject
{
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
}

public record UpdateSettingCommand : IRequest<Result>
{
    public Guid AccountId { get; set; }
    public List<SettingObject> Settings { get; set; } = [];
}

public class UpdateSettingCommandHandler : IRequestHandler<UpdateSettingCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateSettingCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
    {
        var settingDictionary = _context.Settings
           .ToDictionary(s => s.Code, s => s.Id);

        var userSettings = _context.UserSettings.Where(us => us.AccountId == request.AccountId).ToList();
        foreach (var settingObj in request.Settings)
        {
            if (!Enum.IsDefined(typeof(SETTING_KEY), settingObj.Key))
            {
                return Result.Failure(SettingError.InvalidSettingKey);
            }
            if (!ValidateSettingValue(settingObj))
            {
                return Result.Failure(SettingError.InvalidSettingValue);
            }

            var settingId = settingDictionary[settingObj.Key];

            var userSetting = userSettings.SingleOrDefault(us => us.SettingId == settingId);

            // Upsert the settings
            if (userSetting == null)
            {
                _context.UserSettings.Add(new UserSetting
                {
                    AccountId = request.AccountId,
                    SettingId = settingId,
                    SettingValue = settingObj.Value
                });
            }
            else
            {
                userSetting.SettingValue = settingObj.Value;
                _context.UserSettings.Update(userSetting);
            }
        }

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result.Success();
    }

    // This function may need update validation logic as the setting grows
    private bool ValidateSettingValue(SettingObject settingObject)
    {
        //Validate language setting
        if (settingObject.Key == SETTING_KEY.LANGUAGE.ToString())
        {
            return Enum.IsDefined(typeof(SETTING_VALUE.LANGUAGE), settingObject.Value);
        }

        // Validate remaining Boolean setting
        return Enum.IsDefined(typeof(SETTING_VALUE.BOOLEAN), settingObject.Value);
    }


}
