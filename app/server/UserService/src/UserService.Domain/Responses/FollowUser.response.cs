namespace UserService.Domain.Responses;

public class FollowUserResponse
{
    public Guid FollowerId { get; set; }
    public Guid FollowingId { get; set; }
    public bool IsFollowing { get; set; }
}
