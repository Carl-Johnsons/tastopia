using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserService.Domain.Errors;

namespace UserService.Application.Users.Queries;

public class SearchSimpleUserQuery : IRequest<Result<List<Guid>?>>
{
    public string Keyword { get; set; } = null!;
}

public class SearchSimpleUserQueryHandler : IRequestHandler<SearchSimpleUserQuery, Result<List<Guid>?>>
{
    private readonly IApplicationDbContext _context;

    public SearchSimpleUserQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Guid>?>> Handle(SearchSimpleUserQuery request, CancellationToken cancellationToken)
    {
        var keyword = request.Keyword;
        if (string.IsNullOrEmpty(keyword))
        {
            return Result<List<Guid>?>.Failure(UserError.NullParameters, "Keyword is null!");
        }
        keyword = keyword.ToLower();
        var result = await _context.Users.Where(u => u.IsAccountActive && !u.IsAdmin && 
            (
                u.AccountUsername.ToLower().Contains(keyword) ||
                u.DisplayName.ToLower().Contains(keyword)
            )
        ).Select(u => u.AccountId).Distinct().ToListAsync();
        
        if(result == null || result.Count == 0)
        {
            result = [];
        }
        return Result<List<Guid>?>.Success(result);
    }
}
