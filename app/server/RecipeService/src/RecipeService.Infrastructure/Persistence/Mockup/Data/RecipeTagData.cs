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
        TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // BEEF
    },
    new RecipeTag{
        RecipeId = Guid.Parse("1a2b3c4d-7777-8888-9999-000011112222"),
        TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // BEEF
    },
    new RecipeTag{
        RecipeId = Guid.Parse("1a2b3c4d-3333-4444-5555-666677778888"),
        TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // BEEF
    },
    new RecipeTag{
        RecipeId = Guid.Parse("1a2b3c4d-9999-aaaa-bbbb-ccccddddeeee"),
        TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // BEEF
    },
    new RecipeTag{
        RecipeId = Guid.Parse("1a2b3c4d-eeee-ffff-0000-111122223333"),
        TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // BEEF
    },
    new RecipeTag{
        RecipeId = Guid.Parse("2b3c4d5e-aaaa-bbbb-cccc-ddddeeeeffff"),
        TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // BEEF
    },
    new RecipeTag{
        RecipeId = Guid.Parse("2b3c4d5e-1111-2222-3333-444455556666"),
        TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // BEEF
    },
    //red pepper
    new RecipeTag{
        RecipeId = Guid.Parse("2b3c4d5e-aaaa-bbbb-cccc-ddddeeeeffff"),
        TagId = Guid.Parse("6adc1ab3-b10a-4341-881b-553fb7860cc4") // RED PEPPER
    },
    new RecipeTag{
        RecipeId = Guid.Parse("2b3c4d5e-1111-2222-3333-444455556666"),
        TagId = Guid.Parse("6adc1ab3-b10a-4341-881b-553fb7860cc4") // RED PEPPER
    },
    new RecipeTag{
        RecipeId = Guid.Parse("2b3c4d5e-7777-8888-9999-000011112222"),
        TagId = Guid.Parse("6adc1ab3-b10a-4341-881b-553fb7860cc4") // RED PEPPER
    },
    new RecipeTag{
        RecipeId = Guid.Parse("2b3c4d5e-3333-4444-5555-666677778888"),
        TagId = Guid.Parse("6adc1ab3-b10a-4341-881b-553fb7860cc4") // RED PEPPER
    },
    new RecipeTag{
        RecipeId = Guid.Parse("2b3c4d5e-eeee-ffff-0000-111122223333"),
        TagId = Guid.Parse("6adc1ab3-b10a-4341-881b-553fb7860cc4") // RED PEPPER
    },
    //RecipeTag Vuong
    new RecipeTag { RecipeId = Guid.Parse("da9af38a-4662-4d36-b871-b7aa9dad945d"), TagId = Guid.Parse("70033b9c-071b-451e-a9c0-6f182df45955") },
    new RecipeTag { RecipeId = Guid.Parse("15289b6e-b1d8-498e-91e3-44e4fa1d84a2"), TagId = Guid.Parse("70033b9c-071b-451e-a9c0-6f182df45955") },

    new RecipeTag { RecipeId = Guid.Parse("ca5c3915-9965-4cb9-b939-9c940e21c23c"), TagId = Guid.Parse("608b9c05-c5f1-40ca-8bb9-8a1dd76f6d81") },
    new RecipeTag { RecipeId = Guid.Parse("f0d8a591-274c-4da1-b0f0-311c700646ca"), TagId = Guid.Parse("608b9c05-c5f1-40ca-8bb9-8a1dd76f6d81") },

    new RecipeTag { RecipeId = Guid.Parse("be7c6ff0-8eda-4f20-b1a4-3b19212abdf5"), TagId = Guid.Parse("629d1467-b4e4-4910-9203-813216a43db3") },
    new RecipeTag { RecipeId = Guid.Parse("d8f3a1c4-7b9a-4e3b-8c2d-6f5a9d8e1234"), TagId = Guid.Parse("629d1467-b4e4-4910-9203-813216a43db3") },

    new RecipeTag { RecipeId = Guid.Parse("4f547a28-662d-4049-9764-38a02e07e628"), TagId = Guid.Parse("d8270958-d306-421f-9321-fa8ce66a8e95") },
    new RecipeTag { RecipeId = Guid.Parse("b7c66c1b-5762-4dc0-95e1-d51034dc3567"), TagId = Guid.Parse("d8270958-d306-421f-9321-fa8ce66a8e95") },

    new RecipeTag { RecipeId = Guid.Parse("5e0bbc37-9c4d-4469-b0dc-3964ec4c3f06"), TagId = Guid.Parse("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6") },

    new RecipeTag { RecipeId = Guid.Parse("48c4dc32-922c-41f3-8910-480214271e06"), TagId = Guid.Parse("df3f6301-3cae-480a-87da-c7b8f6150292") },

    new RecipeTag { RecipeId = Guid.Parse("49e3d289-e7a4-4811-be9b-75433e69096e"), TagId = Guid.Parse("264aece2-1dd3-4326-87a0-40e22d913b47") },
    new RecipeTag { RecipeId = Guid.Parse("b8837675-31d2-4d5b-ba45-972f8b4ac38f"), TagId = Guid.Parse("264aece2-1dd3-4326-87a0-40e22d913b47") },

    new RecipeTag { RecipeId = Guid.Parse("81349bdd-9713-4325-bd0d-930daaa26e8e"), TagId = Guid.Parse("7437d1c2-fcc0-43f7-9719-a386def772a1") },
    new RecipeTag { RecipeId = Guid.Parse("a9ad629d-6229-4017-87c7-dc30adf12d98"), TagId = Guid.Parse("7437d1c2-fcc0-43f7-9719-a386def772a1") },

    new RecipeTag { RecipeId = Guid.Parse("d6d25c35-1b01-4c6b-b76b-3269e825bf3d"), TagId = Guid.Parse("7d6c2d1c-11fe-4c4a-a87e-2a395c07d834") },
    new RecipeTag { RecipeId = Guid.Parse("0e892d99-767e-4b8b-8d7c-03858628c4f3"), TagId = Guid.Parse("7d6c2d1c-11fe-4c4a-a87e-2a395c07d834") },

    new RecipeTag { RecipeId = Guid.Parse("ffa68fa4-4145-43f9-b476-58131b5a9936"), TagId = Guid.Parse("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e") },

    new RecipeTag { RecipeId = Guid.Parse("6c6167d7-cdc6-4bf8-a363-4f3a8f4634b9"), TagId = Guid.Parse("0e2c473b-1a72-4f52-a2f3-ce399425f185") },
    new RecipeTag { RecipeId = Guid.Parse("81b2a7fe-8dc4-473e-851d-50d535a17e46"), TagId = Guid.Parse("0e2c473b-1a72-4f52-a2f3-ce399425f185") },

    new RecipeTag { RecipeId = Guid.Parse("3ec16a93-2f7b-4890-88c5-3de27c49416f"), TagId = Guid.Parse("31dc282f-d553-43f6-8ba4-ca050e1b8343") },
    new RecipeTag { RecipeId = Guid.Parse("085cd76a-b166-4eef-8382-2aad91e86fbd"), TagId = Guid.Parse("31dc282f-d553-43f6-8ba4-ca050e1b8343") },

    new RecipeTag { RecipeId = Guid.Parse("1ae9fafd-9446-4106-99df-de1517f8606b"), TagId = Guid.Parse("26529e4b-0504-4da6-aa2c-b90232d8ff68") },
    new RecipeTag { RecipeId = Guid.Parse("9c57378a-0e5f-4def-bb60-59e404c1cf9d"), TagId = Guid.Parse("26529e4b-0504-4da6-aa2c-b90232d8ff68") },

    new RecipeTag { RecipeId = Guid.Parse("f56e3110-dc48-4919-a463-12a3fda85724"), TagId = Guid.Parse("31dc282f-d553-43f6-8ba4-ca050e1b8343") },

    //RecipeTag Nhan
    ];
}
