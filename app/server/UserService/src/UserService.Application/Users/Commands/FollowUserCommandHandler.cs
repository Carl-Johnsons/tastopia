using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Domain.Entities;
using UserService.Domain.Errors;
using UserService.Domain.Responses;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace UserService.Application.Users.Commands;

public class FollowUserCommand : IRequest<Result<FollowUserResponse?>>
{
    [Required]
    public Guid? AccountId { get; init; } = null!;
    [Required]
    public Guid? FollowingId { get; init; } = null!;
}

public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, Result<FollowUserResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<FollowUserCommandHandler> _logger;

    public FollowUserCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<FollowUserCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<FollowUserResponse?>> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var accountId = request.AccountId;
            var followingId = request.FollowingId;

            if (accountId == null || accountId == Guid.Empty || followingId == null || followingId == Guid.Empty)
            {
                return Result<FollowUserResponse?>.Failure(UserError.NullParameters, "accountId or followingId not found.");
            }

            var usersDict = await _context.Users.Where(u => u.AccountId == accountId || u.AccountId == followingId).ToDictionaryAsync(u => u.AccountId);


            var follower = usersDict[accountId.Value];
            var following = usersDict[followingId.Value];

            if(follower == null || following == null)
            {
                return Result<FollowUserResponse?>.Failure(UserError.NullParameters, "follower of following not found.");
            }

            var follow = await _context.UserFollows.Where(uf => uf.FollowerId == accountId && uf.FollowingId == followingId).SingleOrDefaultAsync();
            var isFollowing = true;
            if (follow == null)
            {
                follow = new UserFollow
                {
                    FollowerId = accountId.Value,
                    FollowingId = followingId.Value,
                };

                follower.TotalFollowing += 1;
                following.TotalFollower += 1;

                _context.UserFollows.Add(follow);
            }
            else
            {
                isFollowing = false;
                follower.TotalFollowing -= 1;
                following.TotalFollower -= 1;
                _context.UserFollows.Remove(follow);
            }
            await _unitOfWork.SaveChangeAsync();

            return Result<FollowUserResponse?>.Success(new FollowUserResponse
            {
                FollowerId = accountId.Value,
                FollowingId = followingId.Value,
                IsFollowing = isFollowing
            });

        }
        catch (Exception ex)
        {
            _logger.LogError(JsonConvert.SerializeObject(ex));
            return Result<FollowUserResponse?>.Failure(UserError.FollowFail);
        }
    }
}
