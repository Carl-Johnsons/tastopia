using Contract.Event.NotificationEvent;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Application.Notifications.Commands;

public record SendEmailCommand : IRequest<Result>
{
    [Required]
    public string EmailTo { get; set; } = null!;
    [Required]
    public string Subject { get; set; } = null!;
    [Required]
    public string Body { get; set; } = null!;
    public bool IsHTML { get; set; } = false;
}


public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result>
{
    private readonly IServiceBus _serviceBus;

    public SendEmailCommandHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        await _serviceBus.Publish(new SendEmailEvent
        {
            EmailTo = request.EmailTo,
            Subject = request.Subject,
            Body = request.Body,
            IsHTML = request.IsHTML
        });

        return Result.Success();
    }
}
