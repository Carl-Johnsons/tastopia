using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Entities;
using UserService.Domain.Errors;

namespace UserService.Application.Users.Queries;

public record GetSimpleUsersQuery : IRequest<Result<List<User>?>>
{
    [Required]
    public HashSet<Guid> AccountIds { get; init; } = null!;
}
public class GetSimpleUsersQueryHandler : IRequestHandler<GetSimpleUsersQuery, Result<List<User>?>>
{
    private readonly IApplicationDbContext _context;

    public GetSimpleUsersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<User>?>> Handle(GetSimpleUsersQuery request, CancellationToken cancellationToken)
    {
        var accountIds = request.AccountIds;
        if (accountIds == null || accountIds.Count == 0)
        {
            return Result<List<User>?>.Failure(UserError.NotFound);
        }

        var users = await _context.Users
            .Where(user => accountIds.Contains(user.AccountId))
            .Select(user => new User
            {
                AccountId = user.AccountId,
                DisplayName = user.DisplayName,
                AvatarUrl = user.AvatarUrl,
                AccountUsername = user.AccountUsername,
            }).ToListAsync();
        if (users == null || users.Count == 0)
        {
            return Result<List<User>?>.Failure(UserError.NotFound);
        }
        return Result<List<User>?>.Success(users);
    }
}

