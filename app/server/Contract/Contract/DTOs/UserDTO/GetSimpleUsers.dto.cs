namespace Contract.DTOs.UserDTO;

public class GetSimpleUsersDTO
{
    public Dictionary<Guid, SimpleUser> Users { get; set; } = null!;
}

public class SimpleUser
{
    public Guid UserId { get; set; }
    public string AvtUrl { get; set; } = null!;
    public string DisplayName { get; set; } = null!;

}
