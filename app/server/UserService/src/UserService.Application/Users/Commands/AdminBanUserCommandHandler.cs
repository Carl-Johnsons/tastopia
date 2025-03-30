using Contract.Event.IdentityEvent;
using Contract.Event.TrackingEvent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Domain.Errors;
using UserService.Domain.Responses;
namespace UserService.Application.Users.Commands;
public class BanAdminCommand : IRequest<Result<AdminBanUserResponse?>>
{
    public Guid AccountId { get; set; }
    public Guid CurrentAccountId { get; set; }
}
public class BanAdminCommandHandler : IRequestHandler<BanAdminCommand, Result<AdminBanUserResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;
    private readonly ILogger<BanAdminCommandHandler> _logger;
    public BanAdminCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<BanAdminCommandHandler> logger, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _serviceBus = serviceBus;
    }

    public async Task<Result<AdminBanUserResponse?>> Handle(BanAdminCommand request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var currentAccountId = request.CurrentAccountId;

        var user = await _context.Users
             .Where(user => user.AccountId == accountId)
             .FirstOrDefaultAsync();

        if (user == null)
        {
            return Result<AdminBanUserResponse?>.Failure(UserError.NotFound, "Not found admin");
        }

        if (!user.IsAdmin)
        {
            return Result<AdminBanUserResponse?>.Failure(UserError.PermissionDenied);
        }

        var isRestored = !user.IsAccountActive;
        user.IsAccountActive = !user.IsAccountActive;
        _context.Users.Update(user);
        await _unitOfWork.SaveChangeAsync();
        await _serviceBus.Publish(new UpdateAccountIsActiveEvent
        {
            AccountId = accountId,
            IsActive = user.IsAccountActive,
        });

        await _serviceBus.Publish(new AddActivityLogEvent
        {
            AccountId = currentAccountId,
            ActivityType = isRestored ? Contract.Constants.ActivityType.RESTORE : Contract.Constants.ActivityType.DISABLE,
            EntityId = request.AccountId,
            EntityType = Contract.Constants.ActivityEntityType.USER
        });

        return Result<AdminBanUserResponse?>.Success(new AdminBanUserResponse
        {
            AdminId = currentAccountId,
            UserId = accountId,
            User = user,
            IsRestored = isRestored,
        });
    }
}
