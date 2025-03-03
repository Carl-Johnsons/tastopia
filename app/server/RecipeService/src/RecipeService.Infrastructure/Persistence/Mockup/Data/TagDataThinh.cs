using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class TagDataThinh
{
    public static List<Tag> Data = [
        new Tag{
            Id = Guid.Parse("df3f6301-3cae-480a-87da-c7b8f6150292"),
            Code = "TOMATO",
            Value = "Tomato",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/3NovRt2.png",
        },

    ];
}
