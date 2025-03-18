using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Application.Account.Queries;

public class SearchSimpleAccountQuery : IRequest<Result<List<string>?>>
{
    public string Keyword { get; set; } = null!;
}

public class SearchSimpleAccountQueryHandler : IRequestHandler<SearchSimpleAccountQuery, Result<List<string>?>>
{
    private readonly UserManager<ApplicationAccount> _userManager;

    public SearchSimpleAccountQueryHandler(UserManager<ApplicationAccount> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<List<string>?>> Handle(SearchSimpleAccountQuery request, CancellationToken cancellationToken)
    {
        var keyword = request.Keyword;
        if (string.IsNullOrEmpty(keyword))
        {
            return Result<List<string>?>.Failure(AccountError.NotFound, "Keyword is null!");
        }
        keyword = keyword.ToLower();
        var result = await _userManager.Users.Where(u => 
            (
                u.Email!.ToLower().Contains(keyword)
            )
        ).Select(u => u.Id).Distinct().ToListAsync();

        if (result == null || result.Count == 0)
        {
            result = [];
        }
        return Result<List<string>?>.Success(result);
    }
}
