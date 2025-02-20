using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TrackingService.Domain.Entities;
using TrackingService.Domain.Errors;
namespace TrackingService.Application.UserSearchUsers.Commands;

public class DeleteUserSearchUserCommand : IRequest<Result<UserSearchUser?>>
{
    public Guid? AccountId { get; set; }
    public string Keyword { get; set; } = null!;
}

public class DeleteUserSearchUserCommandHandler : IRequestHandler<DeleteUserSearchUserCommand, Result<UserSearchUser?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteUserSearchUserCommandHandler> _logger;

    public DeleteUserSearchUserCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<DeleteUserSearchUserCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<UserSearchUser?>> Handle(DeleteUserSearchUserCommand request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var keyword = request.Keyword;

        if (accountId == null || accountId == Guid.Empty || string.IsNullOrEmpty(keyword))
        {
            return Result<UserSearchUser?>.Failure(UserSearchUserError.NullParameter, "AccountId or Keyword is null.");
        }
        try
        {
            keyword = keyword.ToLower();
            var view = await _context.UserSearchUsers.Where(v => v.Keyword.ToLower() == keyword).FirstOrDefaultAsync();
            if (view == null)
            {
                return Result<UserSearchUser?>.Failure(UserSearchUserError.NotFound, "Not found User keyword to remove.");
            }
            _context.UserSearchUsers.Remove(view);
            await _unitOfWork.SaveChangeAsync();
            return Result<UserSearchUser?>.Success(view);
        }
        catch (Exception ex)
        {
            _logger.LogError(JsonConvert.SerializeObject(ex));
            return Result<UserSearchUser?>.Failure(UserSearchUserError.DeleteUserSearchUserErrorFail);
        }
    }
}


