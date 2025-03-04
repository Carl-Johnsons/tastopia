using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeTagDataThinh
{
    public static List<RecipeTag> Data => [
        // Stir-Fried Chicken with Bitter Melon
        new RecipeTag{
            RecipeId = Guid.Parse("74c4e637-117c-4b66-ab6b-1bb3f16108d4"), // Newly created recipe
            TagId = Guid.Parse("bb1bba5e-3364-4e13-85f3-bb20ded937e6") // Chicken
        },
        new RecipeTag{
            RecipeId = Guid.Parse("74c4e637-117c-4b66-ab6b-1bb3f16108d4"),
            TagId = Guid.Parse("09543e02-e6de-4dc9-bb74-86f829b8db8f") // Bitter Melon
        },
        new RecipeTag{
            RecipeId = Guid.Parse("74c4e637-117c-4b66-ab6b-1bb3f16108d4"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },

        // Chicken Soup with Bamboo Shoots
        new RecipeTag{
            RecipeId = Guid.Parse("5c47a4c1-bca7-4a40-8a8c-6eb79e9f3b56"), // Newly created recipe
            TagId = Guid.Parse("bb1bba5e-3364-4e13-85f3-bb20ded937e6") // Chicken
        },
        new RecipeTag{
            RecipeId = Guid.Parse("5c47a4c1-bca7-4a40-8a8c-6eb79e9f3b56"),
            TagId = Guid.Parse("57fc64be-4067-4303-9ee4-e255978dbc79") // Bamboo Shoots
        },
        new RecipeTag{
            RecipeId = Guid.Parse("5c47a4c1-bca7-4a40-8a8c-6eb79e9f3b56"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },

        // Grilled Lemongrass Chicken
        new RecipeTag{
            RecipeId = Guid.Parse("34f37c02-feb4-4189-b56f-50158aab1c81"), // Newly created recipe
            TagId = Guid.Parse("bb1bba5e-3364-4e13-85f3-bb20ded937e6") // Chicken
        },
        new RecipeTag{
            RecipeId = Guid.Parse("34f37c02-feb4-4189-b56f-50158aab1c81"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },

        // Recipe Tags
        new RecipeTag{
            RecipeId = Guid.Parse("e47f7b1d-8382-4578-9c6f-ce771f709c27"), // Newly created recipe
            TagId = Guid.Parse("c6e9d13a-c6fb-456b-8f4b-90912b91ed7e") // Silkie Chicken
        },
        new RecipeTag{
            RecipeId = Guid.Parse("e47f7b1d-8382-4578-9c6f-ce771f709c27"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
    ];

}
