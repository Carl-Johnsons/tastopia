using RecipeService.Domain.Entities;
namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeTagData {
    public static List<RecipeTag> Data => [
        // Classic Scrambled Eggs
    new RecipeTag{
        RecipeId = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"),
        TagId = Guid.Parse("2bf7f026-e745-4bd9-8701-a9519742d0f7") // EGG
    },
    new RecipeTag{
        RecipeId = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"),
        TagId = Guid.Parse("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3") // MILK
    },
    new RecipeTag{
        RecipeId = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"),
        TagId = Guid.Parse("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1") // BUTTER
    },

    // Tomato Soup
    new RecipeTag{
        RecipeId = Guid.Parse("c8362fc3-5cff-4171-a78d-40613c748596"),
        TagId = Guid.Parse("df3f6301-3cae-480a-87da-c7b8f6150292") // TOMATO
    },
    new RecipeTag{
        RecipeId = Guid.Parse("c8362fc3-5cff-4171-a78d-40613c748596"),
        TagId = Guid.Parse("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e") // GARLIC
    },

    // Pasta Carbonara
    new RecipeTag{
        RecipeId = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
        TagId = Guid.Parse("2bf7f026-e745-4bd9-8701-a9519742d0f7") // EGG
    },
    new RecipeTag{
        RecipeId = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
        TagId = Guid.Parse("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6") // BACON
    },
    new RecipeTag{
        RecipeId = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
        TagId = Guid.Parse("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495") // CHEESE
    },

    // Vegetable Stir-Fry
    new RecipeTag{
        RecipeId = Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
        TagId = Guid.Parse("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e") // CARROT
    },
    new RecipeTag{
        RecipeId = Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
        TagId = Guid.Parse("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6") // BROCCOLI
    },
    new RecipeTag{
        RecipeId = Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
        TagId = Guid.Parse("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8") // SOY_SAUCE
    },

    // Garlic Bread
    new RecipeTag{
        RecipeId = Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac"),
        TagId = Guid.Parse("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e") // GARLIC
    },
    new RecipeTag{
        RecipeId = Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac"),
        TagId = Guid.Parse("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1") // BUTTER
    },
    //ai
    new RecipeTag{
        RecipeId = Guid.Parse("1a2b3c4d-1111-2222-3333-444455556666"),
        TagId = Guid.Parse("921ae2be-5f9c-4611-b296-8a709bb304c8") // BEEF
    },
    new RecipeTag{
        RecipeId = Guid.Parse("1a2b3c4d-7777-8888-9999-000011112222"),
        TagId = Guid.Parse("921ae2be-5f9c-4611-b296-8a709bb304c8") // BEEF
    },
    new RecipeTag{
        RecipeId = Guid.Parse("1a2b3c4d-3333-4444-5555-666677778888"),
        TagId = Guid.Parse("921ae2be-5f9c-4611-b296-8a709bb304c8") // BEEF
    },
    new RecipeTag{
        RecipeId = Guid.Parse("1a2b3c4d-9999-aaaa-bbbb-ccccddddeeee"),
        TagId = Guid.Parse("921ae2be-5f9c-4611-b296-8a709bb304c8") // BEEF
    },
    new RecipeTag{
        RecipeId = Guid.Parse("1a2b3c4d-eeee-ffff-0000-111122223333"),
        TagId = Guid.Parse("921ae2be-5f9c-4611-b296-8a709bb304c8") // BEEF
    },
    ];
}
