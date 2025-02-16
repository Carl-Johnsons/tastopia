using Contract.Utilities;
using Expo.Server.Client;
using Expo.Server.Models;
using Newtonsoft.Json;
using PushNotificationWorker.Interfaces;

namespace PushNotificationWorker.Services;

public class ExpoPushNotificationService : IPushNotificationService
{
    private readonly PushApiClient _pushApiClient;
    private readonly ILogger<ExpoPushNotificationService> _logger;
    public ExpoPushNotificationService(PushApiClient pushApiClient, ILogger<ExpoPushNotificationService> logger)
    {
        EnvUtility.LoadEnvFile();
        _pushApiClient = pushApiClient;
        _logger = logger;
    }

    public async Task Notify(List<string> expoPushTokens, string message, string? data, string? channelId, string? title)
    {
        var pushTicketReq = new PushTicketRequest()
        {
            PushTo = expoPushTokens,
            PushBadgeCount = 7,
            PushBody = message,
            PushData = data,
            PushChannelId = channelId,
            PushTitle = title
        };
        _logger.LogInformation("Request ticket:\n" + JsonConvert.SerializeObject(pushTicketReq, Formatting.Indented));
        var result = await _pushApiClient.PushSendAsync(pushTicketReq);
        _logger.LogInformation(JsonConvert.SerializeObject(result, Formatting.Indented));
    }
}
