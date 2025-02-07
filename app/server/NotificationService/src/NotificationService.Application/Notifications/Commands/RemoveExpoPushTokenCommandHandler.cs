using NotificationService.Domain.Constants;
using NotificationService.Domain.Errors;

namespace NotificationService.Application.Notifications.Commands;

public record RemoveExpoPushTokenCommand : IRequest<Result>
{
    public Guid AccountId { get; set; }
    public DeviceType Type { get; set; }
}

public class RemoveExpoPushTokenCommandHandler : IRequestHandler<RemoveExpoPushTokenCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveExpoPushTokenCommandHandler(IApplicationDbContext context,
                                             IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveExpoPushTokenCommand request,
                               CancellationToken cancellationToken)
    {
        var expoPushTokens = _context.AccountExpoPushTokens
                                  .SingleOrDefault(aet => request.AccountId == aet.AccountId
                                                && request.Type == aet.DeviceType);

        if (expoPushTokens == null)
        {
            return Result.Failure(NotificationErrors.ExpoPushTokenNotFound);
        }
        _context.AccountExpoPushTokens.Remove(expoPushTokens);
        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result.Success();
    }
}
