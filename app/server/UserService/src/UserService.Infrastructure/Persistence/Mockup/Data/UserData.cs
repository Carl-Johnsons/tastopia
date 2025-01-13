using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence.Mockup.Data;

public class UserData
{
    public static List<User> Data = [
            new User{
                AccountId = Guid.Parse("f9a8c16e-610a-49f5-aac0-82183d8c3a16"),
                DisplayName = "Admin",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                AccountUsername = "admin1234",
                IsAdmin = true,
            },
            new User{
                AccountId = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
                DisplayName = "Alice",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 21,
                TotalFollowing = 4,
                AccountUsername = "alice1234"
            },

            new User{
                AccountId = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                DisplayName = "Kian",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalFollwer = 1,
                AccountUsername = "kian1234"
                

            },
            new User{
                AccountId = Guid.Parse("078ecc42-7643-4cff-b851-eeac5ba1bb29"),
                DisplayName = "Bob",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                AccountUsername = "bob1234"

            },
            new User{
                AccountId = Guid.Parse("1cfb7c40-cccc-4a87-88a9-ff967d8dcddb"),
                DisplayName = "Duc",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                AccountUsername = "duc1234"

            },
            new User{
                AccountId = Guid.Parse("50e00c7f-39da-48d1-b273-3562225a5972"),
                DisplayName = "An",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalFollwer = 1,
                AccountUsername = "an1234"

            },
            new User{
                AccountId = Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                DisplayName = "Kara",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                AccountUsername = "kara1234"
            },
            new User{
                AccountId = Guid.Parse("03e4b46e-b84a-43a9-a421-1b19e02023bb"),
                DisplayName = "Raina",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                AccountUsername = "raina1234"
            },
            new User{
                AccountId = Guid.Parse("cd1c7fe9-3308-4afb-83f4-23fa1e9efba8"),
                DisplayName = "Mac",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                AccountUsername = "mac1234"
            },
            new User{
                AccountId = Guid.Parse("76346f0e-a52c-4d94-a909-4a8cc59c8ede"),
                DisplayName = "Lainey",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalFollwer = 1,
                AccountUsername = "lainey1234"

            },
            new User{
                AccountId = Guid.Parse("e797952f-1b76-4db9-81a4-8e2f5f9152ea"),
                DisplayName = "Willa",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalFollwer = 1,
                AccountUsername = "willa1234"
            },
            //more 20 user
            new User {
                AccountId = Guid.Parse("5d02ff8b-62a6-425a-9828-6033112b54e0"),
                DisplayName = "Lily",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "lily"
            },
            new User {
                AccountId = Guid.Parse("8edf9219-7ba6-4259-a7e5-cd95b2e29ca2"),
                DisplayName = "James",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "james"
            },
            new User {
                AccountId = Guid.Parse("6e411b44-26d3-490e-b4e5-8012e2cfd897"),
                DisplayName = "Emma",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "emma"
            },
            new User {
                AccountId = Guid.Parse("bb18d21b-985b-4a54-bc04-f6cf6ac0a32e"),
                DisplayName = "Noah",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "noah"
            },
            new User {
                AccountId = Guid.Parse("9f23334a-1148-4b6e-b636-40cf448735dd"),
                DisplayName = "Ava",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "ava"
            },
            new User {
                AccountId = Guid.Parse("6e898d72-52d0-4de8-a784-5bb1f1a4eda5"),
                DisplayName = "Logan",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "logan"
            },
            new User {
                AccountId = Guid.Parse("6f37441a-92d8-4d27-aa1a-e50ab1a2b4b7"),
                DisplayName = "Sophia",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "sophia"
            },
            new User {
                AccountId = Guid.Parse("9baca8d6-38d7-451e-bf2c-48652ddd7fca"),
                DisplayName = "Lucas",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "lucas"
            },
            new User {
                AccountId = Guid.Parse("69a36c05-c7ff-4411-a283-fa801cbba5ee"),
                DisplayName = "Mia",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "mia"
            },
            new User {
                AccountId = Guid.Parse("e67ac48b-9dd0-42a4-9fa3-a243b00ca5dd"),
                DisplayName = "Ethan",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "ethan"
            },
            new User {
                AccountId = Guid.Parse("7201a43a-6a1d-4634-bc27-9cd71f90a11a"),
                DisplayName = "Isabella",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "isabella"
            },
            new User {
                AccountId = Guid.Parse("0ee35f28-21bf-49f9-ad89-b6a450c41908"),
                DisplayName = "Aiden",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "aiden"
            },
            new User {
                AccountId = Guid.Parse("0f368db8-84f3-499d-be8b-2daf685c6f5e"),
                DisplayName = "Amelia",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "amelia"
            },
            new User {
                AccountId = Guid.Parse("e6333cb5-7008-4fa2-b835-364e304180a3"),
                DisplayName = "Grayson",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "grayson"
            },
            new User {
                AccountId = Guid.Parse("866a7cd5-da2a-46e4-abe3-8efe6bd6a1d0"),
                DisplayName = "Ella",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "ella"
            },
            new User {
                AccountId = Guid.Parse("28fb3b5f-d2a3-4456-a6b5-dbf75cea4e0a"),
                DisplayName = "Jackson",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "jackson"
            },
            new User {
                AccountId = Guid.Parse("2155d0ed-b998-416c-adaf-19f68a0a5b34"),
                DisplayName = "Scarlett",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "scarlett"
            },
            new User {
                AccountId = Guid.Parse("936c85f2-6958-40fd-a201-74485ac917e0"),
                DisplayName = "Alex",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "alex"
            },
            new User {
                AccountId = Guid.Parse("dff9a8f3-c6c4-4d97-98f9-bd9a9a18b0cf"),
                DisplayName = "Chloe",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "chloe"
            },
            new User {
                AccountId = Guid.Parse("b1ccb0c7-34eb-4545-859d-d7307aa42ff7"),
                DisplayName = "Carter",
                AvatarUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png",
                BackgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png",
                TotalRecipe = 0,
                TotalFollowing = 0,
                AccountUsername = "carter"
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
