using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class TagData
{
    public static List<Tag> Data = [
        new Tag
        {
            Id = Guid.Parse("df3f6301-3cae-480a-87da-c7b8f6150292"),
            Code = "TOMATO",
            Value = "Tomato",
            Category = "INGREDIENT",
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/3NovRt2.png",
            //IconUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188128/default_storage/tag/pkain4xbzohdhv7aeeng.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("2bf7f026-e745-4bd9-8701-a9519742d0f7"),
            Code = "EGG",
            Value = "Egg",
            Category = "INGREDIENT",
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/BAT5qyL.png",
            //IconUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188128/default_storage/tag/gcogeajch6fpvqohribk.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("92316e11-fd87-4c0f-aac7-bde4f19c2b38"),
            Code = "RICE",
            Value = "Rice",
            Category = "INGREDIENT",
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/C4nNmU1.png",
            //IconUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188128/default_storage/tag/jendg3sl9ptvhgow2jrx.png",
            CreatedAt= DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("a7a1953d-027e-43b6-ad0d-d10312a3064d"),
            Code = "MUSHROOM",
            Value = "Mushroom",
            Category = "INGREDIENT",
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/m8wBuYO.png",
            //IconUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188127/default_storage/tag/bgbyg81zcbdjpijcn8ep.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        // Additional Tags for Recipes
        new Tag
        {
            Id = Guid.Parse("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3"),
            Code = "MILK",
            Value = "Milk",
            Category = "INGREDIENT",
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/Rk3MwdQ.jpg",
            //IconUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188128/default_storage/tag/mepnrydhtvbsjqw8c0ht.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"),
            Code = "BUTTER",
            Value = "Butter",
            Category = "INGREDIENT",
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/Z8y4Hsr.jpg",
            //IconUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188128/default_storage/tag/te5yyfu8xlldcxygov08.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495"),
            Code = "CHEESE",
            Value = "Cheese",
            Category = "INGREDIENT",
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/feglS7k.jpg",
            //IconUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188128/default_storage/tag/pxx8vnkcs3ibwqubvpa1.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6"),
            Code = "BACON",
            Value = "Bacon",
            Category = "INGREDIENT",
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/lyYgVRi.jpg",
            //IconUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188127/default_storage/tag/cm7vdqacstnodzodzx0o.png",
            CreatedAt= DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e"),
            Code = "GARLIC",
            Value = "Garlic",
            Category = "INGREDIENT",
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/oLwdHvx.jpg",
            //IconUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188127/default_storage/tag/eyy3xy151cyv660ezq1w.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e"),
            Code = "CARROT",
            Value = "Carrot",
            Category = "INGREDIENT",
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/DZEq7TK.jpg",
            //IconUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188127/default_storage/tag/bqnsia9cjejk24bma2sl.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6"),
            Code = "BROCCOLI",
            Value = "Broccoli",
            Category = "INGREDIENT",
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/8nDcffy.png",
            //IconUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188127/default_storage/tag/dcinjpqufvrlanwqyj4g.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8"),
            Code = "SOY_SAUCE",
            Value = "Soy Sauce",
            Category = "INGREDIENT",
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/2QiWJWH.jpg",
            //IconUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188127/default_storage/tag/xc8zoiy2brchcuqgyxhq.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        //Category tag
        new Tag
        {
            Id = Guid.Parse("71676633-493e-46c5-86a0-21773f196035"),
            Code = "ALL",
            Value = "All",
            Category = "DISHTYPE",
            Status = TagStatus.Active,
            ImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196328/default_storage/tag/dishtype/hivalxshuvx5nnlfp5t1.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("6c91b894-d6dd-4c9b-a106-bfd029ce9e16"),
            Code = "NOODLES",
            Value = "Noodles",
            Category = "DISHTYPE",
            Status = TagStatus.Active,
            ImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196327/default_storage/tag/dishtype/ingre9ficyzq0iitiwmd.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("de3698bf-ad8b-4cc6-8ed0-662bd7eca486"),
            Code = "SPICE",
            Value = "Spice",
            Category = "DISHTYPE",
            Status = TagStatus.Active,
            ImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196323/default_storage/tag/dishtype/frgtox8kb4edsvmutmez.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("d8b74fc2-f848-41af-a53f-20170aa453cd"),
            Code = "BBQ",
            Value = "BBQ",
            Category = "DISHTYPE",
            Status = TagStatus.Active,
            ImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196328/default_storage/tag/dishtype/zl1f9g0dxxbtw2f4jiel.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Tag
        {
            Id = Guid.Parse("042dd338-3338-45a1-96c6-a1e2d0ca71ff"),
            Code = "SEAFOOD",
            Value = "Seafood",
            Category = "DISHTYPE",
            Status = TagStatus.Active,
            ImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196329/default_storage/tag/dishtype/jhxdroetbjq9f57cixwj.png",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        }
    ];

}
