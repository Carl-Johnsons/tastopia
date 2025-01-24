using NotificationService.Domain.Constants;
using NotificationService.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Application.Notifications.Commands;

public record SaveExpoPushTokenCommand : IRequest<Result>
{
    [Required]
    public Guid AccountId { get; set; }
    [Required]
    public string ExpoPushToken { get; set; } = null!;
    [Required]
    public DeviceType DeviceType { get; set; }
}

public class SaveExpoPushTokenCommandHandler : IRequestHandler<SaveExpoPushTokenCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public SaveExpoPushTokenCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SaveExpoPushTokenCommand request, CancellationToken cancellationToken)
    {
        var accountPushToken = _context.AccountExpoPushTokens.SingleOrDefault(
                                       a => a.AccountId == request.AccountId &&
                                       a.DeviceType == request.DeviceType);

        if (accountPushToken == null)
        {
            _context.AccountExpoPushTokens.Add(new AccountExpoPushToken
            {
                AccountId = request.AccountId,
                DeviceType = request.DeviceType,
                ExpoPushToken = request.ExpoPushToken,
            });
        }
        else
        {
            accountPushToken.ExpoPushToken = request.ExpoPushToken;
            _context.AccountExpoPushTokens.Update(accountPushToken);
        }
        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result.Success();
    }
}
