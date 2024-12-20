using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class TagData
{
    public static List<Tag> Data => [
        new Tag
        {
            Id = Guid.Parse("df3f6301-3cae-480a-87da-c7b8f6150292"),
            Code = "TOMATO",
            Value = "Tomato",
            Category = "INGREDIENT",
            IsActive = true,
            ImageUrl = "https://i.imgur.com/3NovRt2.png"
        },
        new Tag
        {
            Id = Guid.Parse("2bf7f026-e745-4bd9-8701-a9519742d0f7"),
            Code = "EGG",
            Value = "Egg",
            Category = "INGREDIENT",
            IsActive = true,
            ImageUrl = "https://i.imgur.com/BAT5qyL.png"
        },
        new Tag
        {
            Id = Guid.Parse("92316e11-fd87-4c0f-aac7-bde4f19c2b38"),
            Code = "RICE",
            Value = "Rice",
            Category = "INGREDIENT",
            IsActive = true,
            ImageUrl = "https://i.imgur.com/C4nNmU1.png"
        },
        new Tag
        {
            Id = Guid.Parse("a7a1953d-027e-43b6-ad0d-d10312a3064d"),
            Code = "MUSKROOM",
            Value = "Mushroom",
            Category = "INGREDIENT",
            IsActive = true,
            ImageUrl = "https://i.imgur.com/m8wBuYO.png"
        },
        // Additional Tags for Recipes
        new Tag
        {
            Id = Guid.Parse("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3"),
            Code = "MILK",
            Value = "Milk",
            Category = "INGREDIENT",
            IsActive = true,
            ImageUrl = "https://i.imgur.com/Rk3MwdQ.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"),
            Code = "BUTTER",
            Value = "Butter",
            Category = "INGREDIENT",
            IsActive = true,
            ImageUrl = "https://i.imgur.com/Z8y4Hsr.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495"),
            Code = "CHEESE",
            Value = "Cheese",
            Category = "INGREDIENT",
            IsActive = true,
            ImageUrl = "https://i.imgur.com/feglS7k.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6"),
            Code = "BACON",
            Value = "Bacon",
            Category = "INGREDIENT",
            IsActive = true,
            ImageUrl = "https://i.imgur.com/lyYgVRi.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e"),
            Code = "GARLIC",
            Value = "Garlic",
            Category = "INGREDIENT",
            IsActive = true,
            ImageUrl = "https://i.imgur.com/oLwdHvx.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e"),
            Code = "CARROT",
            Value = "Carrot",
            Category = "INGREDIENT",
            IsActive = true,
            ImageUrl = "https://i.imgur.com/DZEq7TK.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6"),
            Code = "BROCCOLI",
            Value = "Broccoli",
            Category = "INGREDIENT",
            IsActive = true,
            ImageUrl = "https://i.imgur.com/8nDcffy.png"
        },
        new Tag
        {
            Id = Guid.Parse("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8"),
            Code = "SOY_SAUCE",
            Value = "Soy Sauce",
            Category = "INGREDIENT",
            IsActive = true,
            ImageUrl = "https://i.imgur.com/2QiWJWH.jpg"
        }
    ];

}
