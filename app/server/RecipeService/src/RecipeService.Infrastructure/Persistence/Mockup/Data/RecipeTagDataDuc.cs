using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeTagDataDuc
{
    public static List<RecipeTag> Data => [
        // Chinese-style Braised WHite Radish
        new RecipeTag{
            RecipeId = Guid.Parse("262c5dc6-2b2f-4b17-acb7-d8c33bc67c1b"),
            TagId = Guid.Parse("22ddf95e-d898-413d-bad9-945e5cf8a635") // White radish
        },
        new RecipeTag{
            RecipeId = Guid.Parse("262c5dc6-2b2f-4b17-acb7-d8c33bc67c1b"),
            TagId = Guid.Parse("c52ce7e5-7470-4f33-b25f-9fc05f5411c2") // Ginger
        },

        // Blueberry Oat Breakfast Muffins
        new RecipeTag{
            RecipeId = Guid.Parse("310fd400-f2b6-4b22-baa9-68792bc84312"),
            TagId = Guid.Parse("29e47c35-09a0-4b8e-9a0b-066ef14e5921") // Banana
        },
        new RecipeTag{
            RecipeId = Guid.Parse("310fd400-f2b6-4b22-baa9-68792bc84312"),
            TagId = Guid.Parse("c52ce7e5-7470-4f33-b25f-9fc05f5411c2") // Ginger
        },
        new RecipeTag{
            RecipeId = Guid.Parse("310fd400-f2b6-4b22-baa9-68792bc84312"),
            TagId = Guid.Parse("704fb7c2-bd4c-426e-9cec-f86711385e36") // Potato
        },
    ];
}
