using Contract.Event.NotificationEvent;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Application.Notifications.Commands;

public record SendSMSCommand : IRequest<Result>
{
    [Required]
    public string PhoneTo { get; set; } = null!;
    [Required]
    public string Message { get; set; } = null!;
}


public class SendSMSCommandHandler : IRequestHandler<SendSMSCommand, Result>
{
    private readonly IServiceBus _serviceBus;

    public SendSMSCommandHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(SendSMSCommand request, CancellationToken cancellationToken)
    {
        await _serviceBus.Publish(new SendSMSEvent
        {
            PhoneTo = request.PhoneTo,
            Message = request.Message,
        });

        return Result.Success();
    }
}
