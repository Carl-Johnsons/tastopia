using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence.Mockup.Data;

public class UserData
{
    public static List<User> Data = [
            new User{
                Id = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
                DisplayName = "Alice",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
            },

            new User{
                Id = Guid.Parse("d47cb12a-2a20-4151-97c9-d0b85b668799"),
                DisplayName = "Kian",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
            },
        ];
}
