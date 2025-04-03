using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TrackingService.Domain.Entities;
using TrackingService.Domain.Errors;
namespace TrackingService.Application.UserSearchUsers.Commands;

public class CreateUserSearchUserCommand : IRequest<Result>
{
    public Guid? AccountId { get; set; }
    public string Keyword { get; set; } = null!;
    public DateTime? SearchTime { get; set; }
}

public class CreateUserSearchUserCommandHandler : IRequestHandler<CreateUserSearchUserCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateUserSearchUserCommandHandler> _logger;

    public CreateUserSearchUserCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<CreateUserSearchUserCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(CreateUserSearchUserCommand request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var searchTime = request.SearchTime;
        var keyword = request.Keyword;

        if (accountId == null || accountId == Guid.Empty || string.IsNullOrEmpty(keyword) || searchTime == null)
        {
            return Result.Failure(UserSearchUserError.NullParameter);
        }
        try
        {
            var thresholdDate = DateTime.UtcNow.AddMonths(-2);
            var oldViews = await _context.UserSearchUsers
                .Where(v => v.AccountId == accountId && v.CreatedAt < thresholdDate)
                .ToListAsync();
            _context.UserSearchUsers.RemoveRange(oldViews);

            var keytemp = keyword.ToLower();

            var view = await _context.UserSearchUsers.Where(v => v.Keyword.ToLower() == keytemp && v.AccountId == accountId).FirstOrDefaultAsync();

            if (view != null)
            {
                view.CreatedAt = searchTime.Value;
                _context.UserSearchUsers.Update(view);
            }
            else
            {
                view = new UserSearchUser
                {
                    AccountId = accountId.Value,
                    CreatedAt = searchTime.Value,
                    Keyword = keyword,
                };
                _context.UserSearchUsers.Add(view);
            }
            await _unitOfWork.SaveChangeAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(JsonConvert.SerializeObject(ex));
            return Result.Failure(UserSearchUserError.AddUserViewRecipeDetailFail);
        }
    }
}


