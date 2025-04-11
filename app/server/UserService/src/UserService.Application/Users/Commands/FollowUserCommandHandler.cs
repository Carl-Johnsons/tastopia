using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Domain.Entities;
using UserService.Domain.Errors;
using UserService.Domain.Responses;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Contract.Constants;
using Contract.Event.NotificationEvent;

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
    private readonly IServiceBus _serviceBus;

    public FollowUserCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<FollowUserCommandHandler> logger, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _serviceBus = serviceBus;
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

            if (follower == null || following == null)
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

                follower.TotalFollowing = (follower.TotalFollowing ?? 0) + 1;
                following.TotalFollower = (following.TotalFollower ?? 0) + 1;
                _context.Users.Update(follower);
                _context.Users.Update(following);
                _context.UserFollows.Add(follow);
            }
            else
            {
                isFollowing = false;
                follower.TotalFollowing = (follower.TotalFollowing ?? 0) - 1;
                following.TotalFollower = (following.TotalFollower ?? 0) - 1;
                if(follower.TotalFollowing < 0) { follower.TotalFollowing = 0; }
                if(following.TotalFollower < 0) { following.TotalFollower = 0; }
                _context.Users.Update(follower);
                _context.Users.Update(following);
                _context.UserFollows.Remove(follow);
            }
            await _unitOfWork.SaveChangeAsync();
            if (isFollowing)
            {
                await _serviceBus.Publish(new NotifyUserEvent
                {
                    PrimaryActors = [
                        new ActorDTO
                        {
                            ActorId = accountId.Value.ToString(),
                            Type = EntityType.USER
                        }],
                    SecondaryActors = [
                        new ActorDTO
                        {
                            ActorId = followingId.Value.ToString(),
                            Type = EntityType.USER
                        }],
                    TemplateCode = NotificationTemplateCode.USER_FOLLOW,
                    Channels = [NOTIFICATION_CHANNEL.DEFAULT],
                    JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        redirectUri = $"{CLIENT_URI.MOBILE.USER}/{accountId.Value}"
                    }),
                    ImageUrl = follower.AvatarUrl,
                    RecipientIds = [followingId.Value]
                });
            }

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
