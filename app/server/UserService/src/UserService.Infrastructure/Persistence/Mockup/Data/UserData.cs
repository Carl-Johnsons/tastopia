using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence.Mockup.Data;

public class UserData
{
    public static List<User> Data = [
            new User{
                Id = Guid.Parse("f9a8c16e-610a-49f5-aac0-82183d8c3a16"),
                DisplayName = "Admin",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
            },
            new User{
                Id = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
                DisplayName = "Alice",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 21,
                TotalFollowing = 4,
            },

            new User{
                Id = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                DisplayName = "Kian",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalFollwer = 1
            },
            new User{
                Id = Guid.Parse("078ecc42-7643-4cff-b851-eeac5ba1bb29"),
                DisplayName = "Bob",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
            },
            new User{
                Id = Guid.Parse("1cfb7c40-cccc-4a87-88a9-ff967d8dcddb"),
                DisplayName = "Duc",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
            },
            new User{
                Id = Guid.Parse("50e00c7f-39da-48d1-b273-3562225a5972"),
                DisplayName = "An",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalFollwer = 1
            },
            new User{
                Id = Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                DisplayName = "Kara",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
            },
            new User{
                Id = Guid.Parse("03e4b46e-b84a-43a9-a421-1b19e02023bb"),
                DisplayName = "Raina",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
            },
            new User{
                Id = Guid.Parse("cd1c7fe9-3308-4afb-83f4-23fa1e9efba8"),
                DisplayName = "Mac",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
            },
            new User{
                Id = Guid.Parse("76346f0e-a52c-4d94-a909-4a8cc59c8ede"),
                DisplayName = "Lainey",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalFollwer = 1
            },
            new User{
                Id = Guid.Parse("e797952f-1b76-4db9-81a4-8e2f5f9152ea"),
                DisplayName = "Willa",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalFollwer = 1
            },
    ];

    public static List<UserFollow> UserFollowData = [
            new UserFollow{
                FollowerId = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
                FollowingId = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d")
            },
            new UserFollow{
                FollowerId = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
                FollowingId = Guid.Parse("76346f0e-a52c-4d94-a909-4a8cc59c8ede")
            },
            new UserFollow{
                FollowerId = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
                FollowingId = Guid.Parse("e797952f-1b76-4db9-81a4-8e2f5f9152ea")
            },
            new UserFollow{
                FollowerId = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
                FollowingId = Guid.Parse("50e00c7f-39da-48d1-b273-3562225a5972")
            },
    ];
}
