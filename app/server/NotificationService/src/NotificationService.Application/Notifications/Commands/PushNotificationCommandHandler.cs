
using Contract.Event.NotificationEvent;
using MassTransit.Initializers;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using NotificationService.Domain.Errors;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Application.Notifications.Commands;

public record PushNotificationCommand : IRequest<Result>
{
    [Required]
    public string Message { get; init; } = null!;
    public List<Guid> RecipientIds { get; init; } = null!;
    public object? Data { get; init; } = null!;
}


public class PushNotificationCommandHandler : IRequestHandler<PushNotificationCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IServiceBus _serviceBus;

    public PushNotificationCommandHandler(IServiceBus serviceBus, IApplicationDbContext context)
    {
        _serviceBus = serviceBus;
        _context = context;
    }

    public async Task<Result> Handle(PushNotificationCommand request, CancellationToken cancellationToken)
    {
        var expoPushTokens = _context.AccountExpoPushTokens
                                          .Where(aet => request.RecipientIds.Contains(aet.AccountId))
                                          .ToList();

        if (expoPushTokens.Count <= 0)
        {
            return Result.Failure(NotificationErrors.ExpoPushTokenNotFound);
        }
        var tokens = expoPushTokens.Select(ept => ept.ExpoPushToken).ToList();

        await _serviceBus.Publish(new PushNotificationEvent
        {
            ExpoPushTokens = tokens,
            Message = request.Message,
            JsonData = JsonConvert.SerializeObject(request.Data)
        });

        return Result.Success();

    }
}
