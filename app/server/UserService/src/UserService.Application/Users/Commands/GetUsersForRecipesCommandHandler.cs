using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Entities;
using UserService.Domain.Errors;

namespace UserService.Application.Users.Commands;

public record GetUsersForRecipesCommand : IRequest<Result<List<User>?>>
{
    [Required]
    public HashSet<Guid> UserIds { get; init; } = null!;
}
public class GetUsersForRecipesCommandHandler : IRequestHandler<GetUsersForRecipesCommand, Result<List<User>?>>
{
    private readonly IApplicationDbContext _context;

    public GetUsersForRecipesCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
    }

    public async Task<Result<List<User>?>> Handle(GetUsersForRecipesCommand request, CancellationToken cancellationToken)
    {
        var userIds = request.UserIds;
        if(userIds == null || !userIds.Any())
        {
            return Result<List<User>?>.Failure(UserError.NotFound);
        }

        var users = await _context.Users
            .Where(user => userIds.Contains(user.Id))
            .Select(user => new User
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                AvatarUrl = user.AvatarUrl
            }).ToListAsync();
        return Result<List<User>?>.Success(users);
    }
}

