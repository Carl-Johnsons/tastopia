using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NotificationService.Domain.Errors;
namespace NotificationService.Application.Notifications.Commands;

public class SetViewNotificationCommand : IRequest<Result<Recipient?>>
{
    public Guid AccountId { get; set; }
    public Guid NotificationId { get; set; }
}

public class SetViewNotificationCommandHandler : IRequestHandler<SetViewNotificationCommand, Result<Recipient?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public SetViewNotificationCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Recipient?>> Handle(SetViewNotificationCommand request, CancellationToken cancellationToken)
    {
        try {
            var accountId = request.AccountId;
            var notificationId = request.NotificationId;

            if (accountId == Guid.Empty || notificationId == Guid.Empty)
            {
                return Result<Recipient?>.Failure(NotificationErrors.NotFound, "Not found accountId or notificationId.");
            }

            var notification = await _context.Notifications.SingleOrDefaultAsync(n => n.Id == notificationId);

            if (notification == null)
            {
                return Result<Recipient?>.Failure(NotificationErrors.NotFound, "Not found notification.");
            }

            var recipent = notification.Recipients.Where(r => r.RecipientId == accountId).FirstOrDefault();

            if (recipent == null)
            {
                return Result<Recipient?>.Failure(NotificationErrors.NotFound, "Not found recipient in notification.");
            }
            recipent.IsViewed = true;
            _context.Notifications.Update(notification);
            await _unitOfWork.SaveChangeAsync();
            return Result<Recipient?>.Success(recipent);
        }
        catch (Exception ex)
        {
            return Result<Recipient?>.Failure(NotificationErrors.UpdateNotificationFail, ex.Message);
        }

    }
}
