using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Account.Queries;

public record SearchAccountDetailQuery : IRequest<Result<List<ApplicationAccount>?>>
{
    public string Keyword { get; set; } = null!;
}


public class SearchAccountDetailQueryHandler : IRequestHandler<SearchAccountDetailQuery, Result<List<ApplicationAccount>?>>
{
    private readonly UserManager<ApplicationAccount> _userManager;

    public SearchAccountDetailQueryHandler(UserManager<ApplicationAccount> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<List<ApplicationAccount>?>> Handle(SearchAccountDetailQuery request, CancellationToken cancellationToken)
    {
        var keyword = request.Keyword;

        if (string.IsNullOrEmpty(keyword))
        {
            return Result<List<ApplicationAccount>?>.Success([]);
        }

        keyword = keyword.ToLower();

        var acc = await _userManager.Users.Where(a => a.UserName!.ToLower().Contains(keyword)).ToListAsync();

        if (acc == null || !acc.Any())
        {
            return Result<List<ApplicationAccount>?>.Success([]);
        }

        return Result<List<ApplicationAccount>?>.Success(acc);
    }
}
