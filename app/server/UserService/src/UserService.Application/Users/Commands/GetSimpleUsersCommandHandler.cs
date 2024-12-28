using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Entities;
using UserService.Domain.Errors;

namespace UserService.Application.Users.Commands;

public record GetSimpleUsersCommand : IRequest<Result<List<User>?>>
{
    [Required]
    public HashSet<Guid> AccountIds { get; init; } = null!;
}
public class GetSimpleUsersCommandHandler : IRequestHandler<GetSimpleUsersCommand, Result<List<User>?>>
{
    private readonly IApplicationDbContext _context;

    public GetSimpleUsersCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
    }

    public async Task<Result<List<User>?>> Handle(GetSimpleUsersCommand request, CancellationToken cancellationToken)
    {
        var accountIds = request.AccountIds;
        if(accountIds == null || !accountIds.Any())
        {
            return Result<List<User>?>.Failure(UserError.NotFound);
        }

        var users = await _context.Users
            .Where(user => accountIds.Contains(user.AccountId))
            .Select(user => new User
            {
                AccountId = user.AccountId,
                DisplayName = user.DisplayName,
                AvatarUrl = user.AvatarUrl
            }).ToListAsync();
        return Result<List<User>?>.Success(users);
    }
}

