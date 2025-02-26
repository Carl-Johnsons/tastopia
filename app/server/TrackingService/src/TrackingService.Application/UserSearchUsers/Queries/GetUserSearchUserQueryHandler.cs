using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackingService.Application.UserSearchRecipes.Queries;
using TrackingService.Domain.Errors;
namespace TrackingService.Application.UserSearchUsers.Queries;
public class GetUserSearchUserQuery : IRequest<Result<List<string>?>>
{
    public Guid AccountId { get; set; }
}
public class GetUserSearchUserQueryHandler : IRequestHandler<GetUserSearchUserQuery, Result<List<string>?>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetUserSearchRecipeQueryHandler> _logger;
    public GetUserSearchUserQueryHandler(IApplicationDbContext context, ILogger<GetUserSearchRecipeQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Result<List<string>?>> Handle(GetUserSearchUserQuery request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        if (accountId == Guid.Empty)
        {
            return Result<List<string>?>.Failure(UserSearchUserError.NullParameter, "ACCOUNT ID NULL");
        }
        var views = await _context.UserSearchUsers.Where(v => v.AccountId == accountId).OrderByDescending(v => v.CreatedAt).Select(v => v.Keyword).Take(50).ToListAsync();

        if (views == null || views.Count == 0)
        {
            return Result<List<string>?>.Success([]);
        }
        return Result<List<string>?>.Success(views);
    }
}
