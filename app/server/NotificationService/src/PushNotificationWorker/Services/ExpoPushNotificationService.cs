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

    public async Task Notify(List<string> expoPushTokens, string message)
    {
        var pushTicketReq = new PushTicketRequest()
        {
            PushTo = expoPushTokens,
            PushBadgeCount = 7,
            PushBody = message,
        };
        var result = await _pushApiClient.PushSendAsync(pushTicketReq);
        _logger.LogInformation("Done push notification");
        _logger.LogInformation(JsonConvert.SerializeObject(result, Formatting.Indented));
    }
}
