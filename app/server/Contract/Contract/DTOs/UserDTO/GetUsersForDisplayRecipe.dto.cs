namespace Contract.DTOs.UserDTO;

public class GetUsersForDisplayRecipeDTO
{
    public List<UserForDisplayRecipe> Users { get; set; } = null!;
}

public class UserForDisplayRecipe
{
    public Guid UserId { get; set; }
    public string AvtUrl { get; set; } = null!;
    public string DisplayName { get; set; } = null!;

}
