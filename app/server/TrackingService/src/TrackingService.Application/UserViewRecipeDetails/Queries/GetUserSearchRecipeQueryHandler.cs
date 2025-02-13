using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackingService.Domain.Errors;

namespace TrackingService.Application.UserViewRecipeDetails.Queries;

public class GetUserSearchRecipeQuery : IRequest<Result<List<string>?>>
{
    public Guid AccountId { get; set; }
}

public class GetUserSearchRecipeQueryHandler : IRequestHandler<GetUserSearchRecipeQuery, Result<List<string>?>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetUserSearchRecipeQueryHandler> _logger;


    public GetUserSearchRecipeQueryHandler(IApplicationDbContext context, ILogger<GetUserSearchRecipeQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<List<string>?>> Handle(GetUserSearchRecipeQuery request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        if (accountId == Guid.Empty)
        {
            return Result<List<string>?>.Failure(UserViewRecipeDetailError.NotFound, "ACCOUNT ID NULL");
        }
        var views = await _context.UserSearchRecipes.Where(v => v.AccountId == accountId).OrderByDescending(v => v.CreatedAt).Select(v => v.Keyword).Take(50).ToListAsync();
        
        if(views == null || views.Count == 0)
        {
            return Result<List<string>?>.Success([]);
        }
        return Result<List<string>?>.Success(views);
    }
}
