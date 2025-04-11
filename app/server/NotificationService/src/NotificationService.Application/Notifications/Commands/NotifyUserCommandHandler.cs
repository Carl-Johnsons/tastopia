using AutoMapper;
using Contract.Constants;
using Contract.DTOs.SignalRDTO;
using Contract.Event.NotificationEvent;
using Contract.Utilities;
using Google.Protobuf.Collections;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using NotificationService.Domain.Errors;
using RecipeProto;
using SmartFormat;
using UserProto;
using static RecipeProto.GrpcRecipe;

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
    private readonly GrpcRecipeClient _grpcRecipeClient;
    private readonly ISignalRService _signalRService;

    public NotifyUserCommandHandler(IApplicationDbContext context,
                                    IUnitOfWork unitOfWork,
                                    ILogger<NotifyUserCommandHandler> logger,
                                    IServiceBus serviceBus,
                                    GrpcUser.GrpcUserClient grpcUserClient,
                                    IMapper mapper,
                                    ISignalRService signalRService,
                                    GrpcRecipeClient grpcRecipeClient)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _serviceBus = serviceBus;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
        _signalRService = signalRService;
        _grpcRecipeClient = grpcRecipeClient;
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
        var MAX_NAME = 20;
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
            // Get user detail map
            GrpcGetSimpleUsersDTO? grpcUserMap = null;

            var userPrimaryIds = notification.PrimaryActors.Where(a => a.Type == EntityType.USER).Select(a => a.ActorId).ToList();
            var userSecondaryIds = notification.SecondaryActors.Where(a => a.Type == EntityType.USER).Select(a => a.ActorId).ToList();

            var userIdList = userPrimaryIds.Concat(userSecondaryIds).ToList();

            if (userIdList.Count > 0)
            {
                var repeatedField = _mapper.Map<RepeatedField<string>>(userIdList);
                grpcUserMap = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
                {
                    AccountId = { _mapper.Map<RepeatedField<string>>(repeatedField) }
                }, cancellationToken: cancellationToken);
            }

            // Get recipe detail map
            GrpcMapSimpleRecipes? grpcRecipeMap = null;

            var recipePrimaryIds = notification.PrimaryActors.Where(a => a.Type == EntityType.RECIPE).Select(a => a.ActorId).ToList();
            var recipeSecondaryIds = notification.SecondaryActors.Where(a => a.Type == EntityType.RECIPE).Select(a => a.ActorId).ToList();

            var recipeIdList = recipePrimaryIds.Concat(recipeSecondaryIds).ToList();
            if (recipeIdList.Count > 0)
            {
                var repeatedField = _mapper.Map<RepeatedField<string>>(recipeIdList);

                grpcRecipeMap = await _grpcRecipeClient.GetSimpleRecipesAsync(new GrpcGetSimpleRecipeRequest
                {
                    AccountId = request.RecipientIds[0].ToString(), // Dummy Id for this action to work, otherwise this Id has no use
                    RecipeIds = { repeatedField }
                }, cancellationToken: cancellationToken);
            }

            // Get comment detail map
            GrpcMapSimpleComments? grpcCommentMap = null;
            var recipeAndCommentPrimaryIds = notification.PrimaryActors.Where(a => a.Type == EntityType.COMMENT).Select(a => a.ActorId).ToList();
            var recipeAndCommentSecondaryIds = notification.SecondaryActors.Where(a => a.Type == EntityType.COMMENT).Select(a => a.ActorId).ToList();

            var commentAndRecipeIdList = recipeAndCommentPrimaryIds.Concat(recipeAndCommentSecondaryIds).ToList();

            if (commentAndRecipeIdList.Count > 0)
            {
                var repeatedField = _mapper.Map<RepeatedField<string>>(commentAndRecipeIdList);

                grpcCommentMap = await _grpcRecipeClient.GetSimpleCommentsAsync(new GrpcGetSimpleCommentRequest
                {
                    Ids = { repeatedField }
                }, cancellationToken: cancellationToken);
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
                {SETTING_KEY.NOTIFICATION_VOTE.ToString(), [NotificationTemplateCode.USER_UPVOTE.ToString()]},
                {SETTING_KEY.NOTIFICATION_COMMENT.ToString(), [NotificationTemplateCode.USER_COMMENT.ToString()]},
            };

            foreach (var (key, value) in mapUserSettings)
            {
                var paNames = notification.PrimaryActors.Select(pa =>
                {
                    var finalName = "";
                    switch (pa.Type)
                    {
                        case EntityType.USER:
                            finalName = grpcUserMap?.Users[pa.ActorId.ToString()].DisplayName ?? "";
                            break;
                        case EntityType.RECIPE:
                            finalName = grpcRecipeMap?.Recipes[pa.ActorId.ToString()].Title ?? "";
                            break;
                        case EntityType.COMMENT:
                            finalName = grpcCommentMap?.Comments[pa.ActorId].Content ?? "";
                            break;
                    }

                    return finalName.Length > MAX_NAME ? finalName.Substring(0, MAX_NAME) + "..." : finalName;
                }).ToList();

                var saNames = notification.SecondaryActors.Select(sa =>
                {
                    var finalName = "";
                    switch (sa.Type)
                    {
                        case EntityType.USER:
                            finalName = grpcUserMap?.Users[sa.ActorId.ToString()].DisplayName ?? "";
                            break;
                        case EntityType.RECIPE:
                            finalName = grpcRecipeMap?.Recipes[sa.ActorId.ToString()].Title ?? "";
                            break;
                        case EntityType.COMMENT:
                            finalName = grpcCommentMap?.Comments[sa.ActorId].Content ?? "";
                            break;
                    }

                    return finalName.Length > MAX_NAME ? finalName.Substring(0, MAX_NAME) + "..." : finalName;

                }).ToList();
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
