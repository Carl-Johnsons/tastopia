using Contract.Event.IdentityEvent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UserService.Domain.Errors;
using UserService.Domain.Responses;
namespace UserService.Application.Users.Commands;
public class AdminBanUserCommand : IRequest<Result<AdminBanUserResponse?>>
{
    public Guid AccountId { get; set; }
    public Guid CurrentAccountId { get; set; }
}
public class AdminBanUserCommandHandler : IRequestHandler<AdminBanUserCommand, Result<AdminBanUserResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;
    private readonly ILogger<AdminBanUserCommandHandler> _logger;
    public AdminBanUserCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<AdminBanUserCommandHandler> logger, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _serviceBus = serviceBus;
    }

    public async Task<Result<AdminBanUserResponse?>> Handle(AdminBanUserCommand request, CancellationToken cancellationToken)
    {
        try {
            var accountId = request.AccountId;
            var currentAccountId = request.CurrentAccountId;

            if (accountId == Guid.Empty || currentAccountId == Guid.Empty)
            {
                return Result<AdminBanUserResponse?>.Failure(UserError.NullParameters, "Account or CurrentAccountId Id is null");
            }

            var currentUser = await _context.Users
                .Where(user => user.AccountId == currentAccountId)
                .FirstOrDefaultAsync();
            if (currentUser == null)
            {
                return Result<AdminBanUserResponse?>.Failure(UserError.NotFound, "Not found admin");
            }
            if (!currentUser.IsAdmin)
            {
                return Result<AdminBanUserResponse?>.Failure(UserError.PermissionDenied);
            }

            var user = await _context.Users
             .Where(user => user.AccountId == accountId)
             .FirstOrDefaultAsync();

            if (user == null)
            {
                return Result<AdminBanUserResponse?>.Failure(UserError.NotFound, "Not found user");
            }

            if (user.IsAdmin)
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

            return Result<AdminBanUserResponse?>.Success(new AdminBanUserResponse
            {
                AdminId = currentAccountId,
                UserId = accountId,
                User = user,
                IsRestored = isRestored,
            });

        }
        catch (Exception ex) {
            _logger.LogError(JsonConvert.SerializeObject(ex));
            return Result<AdminBanUserResponse?>.Failure(UserError.UpdateUserFail);
        }
        
    }
}
