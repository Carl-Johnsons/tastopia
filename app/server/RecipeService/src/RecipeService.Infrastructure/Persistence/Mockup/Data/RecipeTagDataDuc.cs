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
            RecipeId = Guid.Parse("9c19f806-47c3-4f9e-a1d9-9b4f9c4ed596"),
            TagId = Guid.Parse("c54f00ae-2064-4edb-b246-9da7f45a2735") // Strawberry
        },
        
        // Classic Strawberry Shortcake
        new RecipeTag{
            RecipeId = Guid.Parse("b34206bf-d901-474c-b5f2-563fdb93645e"),
            TagId = Guid.Parse("c54f00ae-2064-4edb-b246-9da7f45a2735") // Strawberry
        },
        new RecipeTag{
            RecipeId = Guid.Parse("b34206bf-d901-474c-b5f2-563fdb93645e"),
            TagId = Guid.Parse("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3") // Milk
        },
        new RecipeTag{
            RecipeId = Guid.Parse("b34206bf-d901-474c-b5f2-563fdb93645e"),
            TagId = Guid.Parse("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1") // Butter
        },

        // Strawberry Banana Smoothie
        new RecipeTag{
            RecipeId = Guid.Parse("fdcec077-b088-4af5-8271-f2f97c191c4b"),
            TagId = Guid.Parse("c54f00ae-2064-4edb-b246-9da7f45a2735") // Strawberry
        },
        new RecipeTag{
            RecipeId = Guid.Parse("fdcec077-b088-4af5-8271-f2f97c191c4b"),
            TagId = Guid.Parse("29e47c35-09a0-4b8e-9a0b-066ef14e5921") // Banana
        },
        new RecipeTag{
            RecipeId = Guid.Parse("fdcec077-b088-4af5-8271-f2f97c191c4b"),
            TagId = Guid.Parse("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3") // Milk
        },

        // Strawberry Basil Bruschetta
        new RecipeTag{
            RecipeId = Guid.Parse("3b6fcddb-daea-47a3-a338-1697d307586d"),
            TagId = Guid.Parse("c54f00ae-2064-4edb-b246-9da7f45a2735") // Strawberry
        },

        //Strawberry Balsamic Chicken
        new RecipeTag{
            RecipeId = Guid.Parse("b3333754-11d3-41a3-bb3b-8010ad1d4e00"),
            TagId = Guid.Parse("c54f00ae-2064-4edb-b246-9da7f45a2735") // Strawberry
        },
        new RecipeTag{
            RecipeId = Guid.Parse("b3333754-11d3-41a3-bb3b-8010ad1d4e00"),
            TagId = Guid.Parse("bb1bba5e-3364-4e13-85f3-bb20ded937e6") // Chicken
        },
        new RecipeTag{
            RecipeId = Guid.Parse("b3333754-11d3-41a3-bb3b-8010ad1d4e00"),
            TagId = Guid.Parse("6adc1ab3-b10a-4341-881b-553fb7860cc4") // Red pepper
        },

        // Hyacinth Bean Sweet Soup
        new RecipeTag{
            RecipeId = Guid.Parse("27d860f7-3625-4df2-bb0b-65da56c64265"),
            TagId = Guid.Parse("c1eec9b0-b2fa-46c4-a367-12424208a0a8") // Hyacinth Bean
        },

        // Vietnamese Green Papaya Salad
        new RecipeTag{
            RecipeId = Guid.Parse("44a7ea08-8e19-4f67-85d1-8bc0dc2a7bef"),
            TagId = Guid.Parse("5c9850d8-414b-4250-8fca-8eecaaa09ece") // Papaya
        },
        new RecipeTag{
            RecipeId = Guid.Parse("44a7ea08-8e19-4f67-85d1-8bc0dc2a7bef"),
            TagId = Guid.Parse("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e") // Carrot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("44a7ea08-8e19-4f67-85d1-8bc0dc2a7bef"),
            TagId = Guid.Parse("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e") // Garlic
        },
        new RecipeTag{
            RecipeId = Guid.Parse("44a7ea08-8e19-4f67-85d1-8bc0dc2a7bef"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        //Vietnamese Papaya Soup with Pork
        new RecipeTag{
            RecipeId = Guid.Parse("9d4e3acd-05ea-4766-bbbd-cd7a06243088"),
            TagId = Guid.Parse("5c9850d8-414b-4250-8fca-8eecaaa09ece") // Papaya
        },
        new RecipeTag{
            RecipeId = Guid.Parse("9d4e3acd-05ea-4766-bbbd-cd7a06243088"),
            TagId = Guid.Parse("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e") // Garlic
        },
        new RecipeTag{
            RecipeId = Guid.Parse("9d4e3acd-05ea-4766-bbbd-cd7a06243088"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("9d4e3acd-05ea-4766-bbbd-cd7a06243088"),
            TagId = Guid.Parse("3d66564d-4146-456c-a1e3-676aa6c872bc") // Pork Ribs
        },

        // Vietnamese Papaya Smoothie
        new RecipeTag{
            RecipeId = Guid.Parse("9a2ffffa-d335-452f-a299-375fd7d8cb58"),
            TagId = Guid.Parse("5c9850d8-414b-4250-8fca-8eecaaa09ece") // Papaya
        },
        new RecipeTag{
            RecipeId = Guid.Parse("9a2ffffa-d335-452f-a299-375fd7d8cb58"),
            TagId = Guid.Parse("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3") // Milk
        },

        // Pineapple Chicken Stir-Fry
        new RecipeTag{
            RecipeId = Guid.Parse("b7703e62-9271-4790-ac6c-309e9e32653a"),
            TagId = Guid.Parse("bb1bba5e-3364-4e13-85f3-bb20ded937e6") // Chicken
        },
        new RecipeTag{
            RecipeId = Guid.Parse("b7703e62-9271-4790-ac6c-309e9e32653a"),
            TagId = Guid.Parse("6adc1ab3-b10a-4341-881b-553fb7860cc4") // Red pepper
        },
        new RecipeTag{
            RecipeId = Guid.Parse("b7703e62-9271-4790-ac6c-309e9e32653a"),
            TagId = Guid.Parse("5fd67837-875d-4c0a-a1fb-3a2c84e0d712") // Pineapple
        },
        new RecipeTag{
            RecipeId = Guid.Parse("b7703e62-9271-4790-ac6c-309e9e32653a"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green onion
        },
        new RecipeTag{
            RecipeId = Guid.Parse("b7703e62-9271-4790-ac6c-309e9e32653a"),
            TagId = Guid.Parse("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e") // Garlic
        },

        // Vietnamese Sweet & Sour Soup
        new RecipeTag{
            RecipeId = Guid.Parse("0f1281b5-608a-4df8-a4b6-605aae8abbf8"),
            TagId = Guid.Parse("bb1bba5e-3364-4e13-85f3-bb20ded937e6") // Chicken
        },
        new RecipeTag{
            RecipeId = Guid.Parse("0f1281b5-608a-4df8-a4b6-605aae8abbf8"),
            TagId = Guid.Parse("5fd67837-875d-4c0a-a1fb-3a2c84e0d712") // Pineapple
        },
        new RecipeTag{
            RecipeId = Guid.Parse("0f1281b5-608a-4df8-a4b6-605aae8abbf8"),
            TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
        },
        new RecipeTag{
            RecipeId = Guid.Parse("0f1281b5-608a-4df8-a4b6-605aae8abbf8"),
            TagId = Guid.Parse("d653e6fc-200b-478b-a593-0cfc490de97f") // Okra
        },
        new RecipeTag{
            RecipeId = Guid.Parse("0f1281b5-608a-4df8-a4b6-605aae8abbf8"),
            TagId = Guid.Parse("df3f6301-3cae-480a-87da-c7b8f6150292") // Tomato
        },
        new RecipeTag{
            RecipeId = Guid.Parse("0f1281b5-608a-4df8-a4b6-605aae8abbf8"),
            TagId = Guid.Parse("2449f48e-71f4-4ecc-b70e-1fa51c2e107f") // Mungbean Sprout
        },

        // Shrimps, bean sprouts and watermelon salad
        new RecipeTag{
            RecipeId = Guid.Parse("027157a0-866f-487a-98b7-e94f4bfcc300"),
            TagId = Guid.Parse("2449f48e-71f4-4ecc-b70e-1fa51c2e107f") // Mungbean Sprout
        },
        new RecipeTag{
            RecipeId = Guid.Parse("027157a0-866f-487a-98b7-e94f4bfcc300"),
            TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
        },
        new RecipeTag{
            RecipeId = Guid.Parse("027157a0-866f-487a-98b7-e94f4bfcc300"),
            TagId = Guid.Parse("613b4247-4360-42ad-b866-8619c694598f") // Watermelon
        },
        new RecipeTag{
            RecipeId = Guid.Parse("027157a0-866f-487a-98b7-e94f4bfcc300"),
            TagId = Guid.Parse("d319ba66-c185-4f7a-9a53-509189791baa") // Lettuce
        },

        // Vietnamese Watermelon Smoothie
        new RecipeTag{
            RecipeId = Guid.Parse("77eaa718-0346-4d79-8f3f-5dd6dbf9d615"),
            TagId = Guid.Parse("613b4247-4360-42ad-b866-8619c694598f") // Watermelon
        },

        // Chilled Cantaloupe Soup
        new RecipeTag{
            RecipeId = Guid.Parse("a5c8f8c8-97c5-4047-b28b-1b8ab377cbbf"),
            TagId = Guid.Parse("46fc8651-4374-4bbe-b695-95dd698fd341") // Cantaloupe
        },

        // Crisp Fried Pig's Tail
        new RecipeTag{
            RecipeId = Guid.Parse("52c0c17c-6837-4f6b-a6db-9a7545c4075d"),
            TagId = Guid.Parse("3ad3bd53-aee7-4179-b879-5d6a9dfd3d7d") // Pig's Tail
        },
        new RecipeTag{
            RecipeId = Guid.Parse("52c0c17c-6837-4f6b-a6db-9a7545c4075d"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green onion
        },
        new RecipeTag{
            RecipeId = Guid.Parse("52c0c17c-6837-4f6b-a6db-9a7545c4075d"),
            TagId = Guid.Parse("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e") // Garlic
        },

        // Vietnamese Baked Banana Cake
        new RecipeTag{
            RecipeId = Guid.Parse("2b94a458-0ded-40d1-b17b-148cde3c3d37"),
            TagId = Guid.Parse("29e47c35-09a0-4b8e-9a0b-066ef14e5921") // Banana
        },
        new RecipeTag{
            RecipeId = Guid.Parse("2b94a458-0ded-40d1-b17b-148cde3c3d37"),
            TagId = Guid.Parse("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1") // Butter
        },
        new RecipeTag{
            RecipeId = Guid.Parse("2b94a458-0ded-40d1-b17b-148cde3c3d37"),
            TagId = Guid.Parse("2bf7f026-e745-4bd9-8701-a9519742d0f7") // Egg
        },
        new RecipeTag{
            RecipeId = Guid.Parse("2b94a458-0ded-40d1-b17b-148cde3c3d37"),
            TagId = Guid.Parse("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3") // Milk
        },

        // Vietnamese Banana Smoothie
        new RecipeTag{
            RecipeId = Guid.Parse("7a3adfb0-7848-49f0-bf5c-b0db5d8f9bee"),
            TagId = Guid.Parse("29e47c35-09a0-4b8e-9a0b-066ef14e5921") // Banana
        },
        new RecipeTag{
            RecipeId = Guid.Parse("7a3adfb0-7848-49f0-bf5c-b0db5d8f9bee"),
            TagId = Guid.Parse("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3") // Milk
        },

        // White Radish Soup
        new RecipeTag{
            RecipeId = Guid.Parse("f06dba1e-b786-4fa0-8ff9-4261fd0dafc2"),
            TagId = Guid.Parse("22ddf95e-d898-413d-bad9-945e5cf8a635") // White Radish
        },
        new RecipeTag{
            RecipeId = Guid.Parse("f06dba1e-b786-4fa0-8ff9-4261fd0dafc2"),
            TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // Beef
        },

        // Vietnamese Fried Radish Rice Cakes
        new RecipeTag{
            RecipeId = Guid.Parse("a9f86c25-5f24-45c8-a09b-5fc29dab8936"),
            TagId = Guid.Parse("22ddf95e-d898-413d-bad9-945e5cf8a635") // White Radish
        },
        new RecipeTag{
            RecipeId = Guid.Parse("a9f86c25-5f24-45c8-a09b-5fc29dab8936"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },
        new RecipeTag{
            RecipeId = Guid.Parse("a9f86c25-5f24-45c8-a09b-5fc29dab8936"),
            TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
        },
    ];
}
