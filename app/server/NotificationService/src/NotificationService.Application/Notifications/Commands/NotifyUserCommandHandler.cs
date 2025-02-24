using AutoMapper;
using Contract.Constants;
using Contract.DTOs.SignalRDTO;
using Contract.DTOs.UserDTO;
using Contract.Event.NotificationEvent;
using Contract.Utilities;
using Google.Protobuf.Collections;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using NotificationService.Domain.Errors;
using SmartFormat;
using UserProto;

namespace NotificationService.Application.Notifications.Commands;

public record NotifyUserCommand : IRequest<Result>
{
    public List<Actor> PrimaryActors { get; set; } = [];
    public List<Actor> SecondaryActors { get; set; } = [];
    public List<Guid> RecipientIds { get; set; } = [];
    public NotificationTemplateCode TemplateCode { get; set; }
    public List<string> Channels { get; set; } = [];
    public string? JsonData { get; set; }
    public string? ImageUrl { get; set; }
}

public class NotifyUserCommandHandler : IRequestHandler<NotifyUserCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;
    private readonly IMapper _mapper;
    private readonly ILogger<NotifyUserCommandHandler> _logger;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly ISignalRService _signalRService;

    public NotifyUserCommandHandler(IApplicationDbContext context,
                                    IUnitOfWork unitOfWork,
                                    ILogger<NotifyUserCommandHandler> logger,
                                    IServiceBus serviceBus,
                                    GrpcUser.GrpcUserClient grpcUserClient,
                                    IMapper mapper,
                                    ISignalRService signalRService)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _serviceBus = serviceBus;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
        _signalRService = signalRService;
    }

    private record UserSettingObj
    {
        public string Code { get; set; } = null!;
        public string Value { get; set; } = null!;
        public SettingDataType DataType { get; set; }
    }

    public async Task<Result> Handle(NotifyUserCommand request,
                               CancellationToken cancellationToken)
    {
        var channels = request.Channels;
        var jsonData = request.JsonData;
        if (channels.Count == 0)
        {
            channels.Add(NOTIFICATION_CHANNEL.DEFAULT);
        }

        jsonData ??= "{\"redirectUri\":\"" + CLIENT_URI.MOBILE.NOTIFICATION + "\"}";

        var template = _context.NotificationTemplates.SingleOrDefault(nt => nt.TemplateCode.ToString()
                                                                              == request.TemplateCode.ToString());
        if (template == null)
        {
            return Result.Failure(NotificationErrors.TemplateNotFound);
        }
        var recipients = request.RecipientIds.Select(id => new Recipient { RecipientId = id, IsViewed = false }).ToList();
        var notification = new Notification
        {
            PrimaryActors = request.PrimaryActors,
            SecondaryActors = request.SecondaryActors,
            JsonData = jsonData,
            TemplateId = template.Id,
            ImageUrl = request.ImageUrl,
            Recipients = recipients
        };

        _context.Notifications.Add(notification);

        await _unitOfWork.SaveChangeAsync(cancellationToken);
        await _signalRService.InvokeAction(SignalRAction.InvalidateNotification.ToString(), new InvalidateNotificationDTO
        {
            RecipientIds = request.RecipientIds
        });
        _logger.LogInformation(JsonConvert.SerializeObject(notification, Formatting.Indented));

        // Push the notification to mobile user
        List<Guid> recipientIds = request.RecipientIds.ToList();

        var expoPushTokens = _context.AccountExpoPushTokens
                                  .Where(aet => recipientIds.Contains(aet.AccountId))
                                  .ToList();

        if (expoPushTokens.Count <= 0)
        {
            _logger.LogWarning("There are no expo push token associating with these accounts id");
        }
        else
        {
            var actorIdSets = notification.PrimaryActors.Concat(notification.SecondaryActors)
                                                        .Select(merge => merge.ActorId.ToString())
                                                        .ToHashSet();
            var res = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
            {
                AccountId = { _mapper.Map<RepeatedField<string>>(actorIdSets) }
            }, cancellationToken: cancellationToken);

            var mapUsers = new Dictionary<Guid, SimpleUser>();

            foreach (var (key, value) in res.Users)
            {
                mapUsers[Guid.Parse(key)] = new SimpleUser
                {
                    AccountId = Guid.Parse(value.AccountId),
                    AvtUrl = value.AvtUrl,
                    DisplayName = value.DisplayName,
                };
            }

            if (res == null || mapUsers.Count != actorIdSets.Count)
            {
                return Result.Failure(NotificationErrors.NotFound, "Actor not found for push notification");
            }

            var recipientIdSet = notification.Recipients.Select(merge => merge.RecipientId.ToString())
                                                             .ToHashSet();

            var settingRes = await _grpcUserClient.GetUserSettingAsync(new GrpcGetUserSettingRequest
            {
                AccountId = { _mapper.Map<RepeatedField<string>>(recipientIdSet) }
            }, cancellationToken: cancellationToken);

            var mapUserSettings = new Dictionary<Guid, List<UserSettingObj>>();

            foreach (var (key, value) in settingRes.SettingMap)
            {
                List<UserSettingObj> userSetting = [];
                foreach (var setting in value.Settings)
                {
                    if (setting.SettingCode == SETTING_KEY.LANGUAGE.ToString())
                    {
                        var languageSetting = LanguageUtility.ToIso6391(value.Settings.SingleOrDefault(s => s.SettingCode == SETTING_KEY.LANGUAGE.ToString())?.SettingValue
                                                    ?? "en");
                        userSetting.Add(new UserSettingObj
                        {
                            Code = SETTING_KEY.LANGUAGE.ToString(),
                            Value = languageSetting,
                            DataType = (SettingDataType)Enum.Parse(typeof(SettingDataType), setting.SettingType)
                        });
                        continue;
                    }

                    userSetting.Add(new UserSettingObj
                    {
                        Code = setting.SettingCode,
                        Value = setting.SettingValue,
                        DataType = (SettingDataType)Enum.Parse(typeof(SettingDataType), setting.SettingType)
                    });
                }

                mapUserSettings[Guid.Parse(key)] = userSetting;
            }

            var mapSettingNotificationTemplate = new Dictionary<string, List<string>> {
                {SETTING_KEY.NOTIFICATION_FOLLOW.ToString(), [NotificationTemplateCode.USER_FOLLOW.ToString()]},
                {SETTING_KEY.NOTIFICATION_VOTE.ToString(), [NotificationTemplateCode.USER_DOWNVOTE.ToString(),
                                                            NotificationTemplateCode.USER_UPVOTE.ToString()]},
                {SETTING_KEY.NOTIFICATION_COMMENT.ToString(), [NotificationTemplateCode.USER_COMMENT.ToString()]},
            };


            foreach (var (key, value) in mapUserSettings)
            {
                var paNames = notification.PrimaryActors.Select(pa => mapUsers[pa.ActorId].DisplayName).ToList();
                var saNames = notification.SecondaryActors.Select(sa => mapUsers[sa.ActorId].DisplayName).ToList();
                var message = template.TranslationMessages.GetValueOrDefault(value.SingleOrDefault(v => v.Code
                                                                                                        == SETTING_KEY.LANGUAGE.ToString())!.Value)
                              ?? "";
                var title = notification.Template?.TranslationTitles?.GetValueOrDefault(value.SingleOrDefault(v => v.Code
                                                                                                                   == SETTING_KEY.LANGUAGE.ToString())!.Value)
                            ?? "";

                bool shouldNotify = true;

                foreach (var setting in value)
                {
                    if (setting.DataType.ToString() == SettingDataType.Boolean.ToString()
                        && mapSettingNotificationTemplate.ContainsKey(setting.Code)
                        && mapSettingNotificationTemplate[setting.Code].Contains(request.TemplateCode.ToString())
                        && !Boolean.Parse(value.SingleOrDefault(v => v.Code == setting.Code)!.Value))
                    {
                        shouldNotify = false;
                        break;
                    }
                }


                if (!shouldNotify)
                {
                    _logger.LogInformation($"Should notify {shouldNotify} with template {request.TemplateCode.ToString()}");
                    continue;
                }

                var data = new
                {
                    Actors = paNames,
                    Targets = saNames,
                    IsSelf = true
                };

                message = Smart.Format(message, data);
                title = Smart.Format(title);

                var tokens = expoPushTokens.Select(ept => ept.ExpoPushToken).ToList();

                foreach (var channel in channels)
                {
                    await _serviceBus.Publish(new PushNotificationEvent
                    {
                        ExpoPushTokens = tokens,
                        Message = message,
                        JsonData = jsonData,
                        ChannelId = channel,
                        Title = title,
                    });
                }
            }
        }

        return Result.Success();
    }
}
