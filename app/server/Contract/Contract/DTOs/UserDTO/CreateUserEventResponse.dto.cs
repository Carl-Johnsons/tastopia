namespace Contract.DTOs.UserDTO;

public class CreateUserEventResponseDTO
{
    public Guid Guid { get; set; }
    public string DisplayName { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    public string BackgroundUrl { get; set; } = null!;
}
