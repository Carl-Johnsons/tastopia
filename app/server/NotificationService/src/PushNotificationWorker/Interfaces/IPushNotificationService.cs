﻿namespace PushNotificationWorker.Interfaces;

public interface IPushNotificationService
{
    Task Notify(List<string> expoPushTokens, string message, string? data, string? channelId, string? title);
}