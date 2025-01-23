
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Application.Notifications.Commands;

public record PushNotificationCommand : IRequest<Result>
{
    [Required]
    public Guid AccountId { get; init; }
    [Required]
    public string Message { get; init; } = null!;
}


public class PushNotificationCommandHandler : IRequestHandler<PushNotificationCommand, Result>
{
    private readonly IServiceBus _serviceBus;

    public PushNotificationCommandHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(PushNotificationCommand request, CancellationToken cancellationToken)
    {


        //await _serviceBus.Publish(new PushNotificationEvent
        //{
        //    ExpoPushToken = request.ExpoPushToken,
        //    Message = request.Message
        //});

        return Result.Success();
    }
}
