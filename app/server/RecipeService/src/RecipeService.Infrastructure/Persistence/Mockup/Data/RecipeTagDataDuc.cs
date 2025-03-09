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

        // Paula Deen's Okra Fritters
        new RecipeTag{
            RecipeId = Guid.Parse("0ec9266c-1380-480e-9be7-b4f86e5b2252"),
            TagId = Guid.Parse("d653e6fc-200b-478b-a593-0cfc490de97f") // Okra
        },

        // Spicy Stir-Fried Chinese Long Beans
        new RecipeTag{
            RecipeId = Guid.Parse("e348525a-d9bd-4d0a-a7ee-0d2a9c9ae1cf"),
            TagId = Guid.Parse("8dfdfff4-ce63-42c5-aa2c-dc7b4904b943") // Long bean
        },
        new RecipeTag{
            RecipeId = Guid.Parse("e348525a-d9bd-4d0a-a7ee-0d2a9c9ae1cf"),
            TagId = Guid.Parse("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6") // Bacon
        },
        new RecipeTag{
            RecipeId = Guid.Parse("e348525a-d9bd-4d0a-a7ee-0d2a9c9ae1cf"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        // Stir-Fried Garlic String Beans (3 Ingredients Only)
        new RecipeTag{
            RecipeId = Guid.Parse("2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e"),
            TagId = Guid.Parse("23d9ad45-e302-431d-9b0f-66b3b5ab0e73") // String Bean
        },
        new RecipeTag{
            RecipeId = Guid.Parse("2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e"),
            TagId = Guid.Parse("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e") // Garlic
        },
        // Chinese Garlic String Beans
        new RecipeTag{
            RecipeId = Guid.Parse("b733e46a-d9c0-4d18-a48e-b95e4433aaa7"),
            TagId = Guid.Parse("23d9ad45-e302-431d-9b0f-66b3b5ab0e73") // String Bean
        },
        new RecipeTag{
            RecipeId = Guid.Parse("b733e46a-d9c0-4d18-a48e-b95e4433aaa7"),
            TagId = Guid.Parse("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e") // Garlic
        },
        // Strawberry Spinach Salad with Poppy Seed Dressing
        new RecipeTag{
            RecipeId = Guid.Parse("a1b2c3d4-e5f6-7890-ab12-cd34ef56gh78"),
            TagId = Guid.Parse("c54f00ae-2064-4edb-b246-9da7f45a2735") // Strawberry
        },
        // Classic Strawberry Shortcake
        new RecipeTag{
            RecipeId = Guid.Parse("b2c3d4e5-f6a7-8901-bc23-de45fg67hi89"),
            TagId = Guid.Parse("c54f00ae-2064-4edb-b246-9da7f45a2735") // Strawberry
        },
    ];
}
