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

    //RecipeTag Long
    new RecipeTag {
    RecipeId = Guid.Parse("809aedc6-da3c-4d40-9e04-23fec17ced58"),
    TagId = Guid.Parse(
        "2212127c-069e-4ac5-97e5-7fca16819c7a") // King Trumpet Mushroom
    },
    new RecipeTag {
    RecipeId = Guid.Parse("3cd271e8-6da9-48e5-a86f-92a841428099"),
    TagId = Guid.Parse(
        "2212127c-069e-4ac5-97e5-7fca16819c7a") // King Trumpet Mushroom
    },

    // Shiitake Mushroom Recipes
    new RecipeTag {
    RecipeId = Guid.Parse("698b75da-7407-4f52-b8ef-ff50abeb71dd"),
    TagId = Guid.Parse(
        "e61a8226-4c90-4f96-9b5d-b80268345623") // Shiitake Mushrooms
    },
    new RecipeTag {
    RecipeId = Guid.Parse("3e794493-78c9-4f59-b6e3-3dae0989d314"),
    TagId = Guid.Parse(
        "e61a8226-4c90-4f96-9b5d-b80268345623") // Shiitake Mushrooms
    },

    // Enoki Mushroom Recipes
    new RecipeTag {
    RecipeId = Guid.Parse("6192c1bc-f8f4-4f15-9574-79fe3a544fd8"),
    TagId =
        Guid.Parse("e0c01336-f339-46c3-903b-89fb1b6b5505") // Enoki mushroom
    },
    new RecipeTag {
    RecipeId = Guid.Parse("adbfc35d-e0f5-471b-a97f-8762e3584aeb"),
    TagId =
        Guid.Parse("e0c01336-f339-46c3-903b-89fb1b6b5505") // Enoki mushroom
    },

    // Black Fungus Recipes
    new RecipeTag {
    RecipeId = Guid.Parse("2c7c4a1f-f6a0-435e-9703-d30e4be80f5c"),
    TagId =
        Guid.Parse("2fa8f0fa-87d3-4ed0-8034-8a0fcb43e718") // Black fungus
    },
    new RecipeTag {
    RecipeId = Guid.Parse("c0a09d9c-299d-4214-827a-c0ce8e016c52"),
    TagId =
        Guid.Parse("2fa8f0fa-87d3-4ed0-8034-8a0fcb43e718") // Black fungus
    },

    // Flower Snails Recipes
    new RecipeTag {
    RecipeId = Guid.Parse("b0082062-dc9f-4c8c-86c4-a991f086e97c"),
    TagId =
        Guid.Parse("c00c8daa-870b-41d3-90a8-a706e34c3096") // Flower Snails
    },
    new RecipeTag {
    RecipeId = Guid.Parse("f9a698fa-7104-47fe-afec-d983cfa8d637"),
    TagId =
        Guid.Parse("c00c8daa-870b-41d3-90a8-a706e34c3096") // Flower Snails
    },

    // Bell Pepper Recipes
    new RecipeTag {
    RecipeId = Guid.Parse("973ce668-0ef9-474b-b1de-3de84f817075"),
    TagId =
        Guid.Parse("02131c94-db6a-470c-ad4c-507b1ff1c6aa") // Bell Pepper
    },
    new RecipeTag {
    RecipeId = Guid.Parse("a4e35661-acec-4c7d-9026-6f6c2d1d4607"),
    TagId =
        Guid.Parse("02131c94-db6a-470c-ad4c-507b1ff1c6aa") // Bell Pepper
    },

    // Lemongrass Recipes
    new RecipeTag {
    RecipeId = Guid.Parse("1b4d8f34-5a70-4370-9255-6e7f6dd3e5f0"),
    TagId = Guid.Parse("3f5093c0-b059-4210-8a4f-bb92f80500d0") // Lemongrass
    },
    new RecipeTag {
    RecipeId = Guid.Parse("c93e0c70-8ea9-4606-9f84-bfbd3782c78b"),
    TagId = Guid.Parse("3f5093c0-b059-4210-8a4f-bb92f80500d0") // Lemongrass
    },

    // Pork Ribs Recipes
    new RecipeTag {
    RecipeId = Guid.Parse("cc1cca39-2c3c-4dce-a487-466d52faaf39"),
    TagId = Guid.Parse("3d66564d-4146-456c-a1e3-676aa6c872bc") // Prok Ribs
    },
    new RecipeTag {
    RecipeId = Guid.Parse("50aaa4bd-d682-4df6-bb36-682915b91737"),
    TagId = Guid.Parse("3d66564d-4146-456c-a1e3-676aa6c872bc") // Prok Ribs
    },
    new RecipeTag {
    RecipeId = Guid.Parse("9e8cbee2-9275-4d8d-9e0a-7454e4cb525c"),
    TagId = Guid.Parse("3d66564d-4146-456c-a1e3-676aa6c872bc") // Prok Ribs
    },
    new RecipeTag {
    RecipeId = Guid.Parse("de3b30cd-cc23-4c6e-a1ca-262f076d009b"),
    TagId = Guid.Parse("3d66564d-4146-456c-a1e3-676aa6c872bc") // Prok Ribs
    },

    // Beef Recipes
    new RecipeTag {
    RecipeId = Guid.Parse("b5c2bfe4-d3e9-48d5-a9a5-c92c445bc9e5"),
    TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // Beef
    },
    new RecipeTag {
    RecipeId = Guid.Parse("72bd5fac-18d8-496a-a5ab-952a2a9a43e5"),
    TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // Beef
    },

    // Ground Pork Recipes
    new RecipeTag {
    RecipeId = Guid.Parse("4e1723cb-2227-43f9-a700-41485450a139"),
    TagId =
        Guid.Parse("644283e4-982c-42b0-99d0-c0183759a820") // Ground Pork
    },
    new RecipeTag {
    RecipeId = Guid.Parse("b87b86bd-7f60-407e-a106-10f049890c1d"),
    TagId =
        Guid.Parse("644283e4-982c-42b0-99d0-c0183759a820") // Ground Pork
    },

    // Chicken Heart Recipes
    new RecipeTag {
    RecipeId = Guid.Parse("70b9fd56-2ea3-4fc9-9477-75d7749fc259"),
    TagId =
        Guid.Parse("0e24b473-9d14-40a5-b239-0666c6c0a920") // Chicken Heart
    },
    new RecipeTag {
    RecipeId = Guid.Parse("31fd92db-c271-421a-adad-0b8c8455972e"),
    TagId =
        Guid.Parse("0e24b473-9d14-40a5-b239-0666c6c0a920") // Chicken Heart
    },

    // Shrimp Recipes
    new RecipeTag {
    RecipeId = Guid.Parse("aec08a9e-b7ce-466c-a7e1-877b7c94dcc9"),
    TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
    },
    new RecipeTag {
    RecipeId = Guid.Parse("485ce44f-4764-40e8-b531-86771e9ab0eb"),
    TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
    },
    new RecipeTag {
    RecipeId = Guid.Parse("ef388ae9-c38c-4b0b-9ef4-eb076b313b49"),
    TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
    },
    new RecipeTag {
    RecipeId = Guid.Parse("04788402-e7b0-4a60-a550-9073859a2857"),
    TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
    },

    // Lettuce Recipes
    new RecipeTag {
    RecipeId = Guid.Parse("96c0c736-c0bc-464a-b57f-e98e9b36ff11"),
    TagId = Guid.Parse("d319ba66-c185-4f7a-9a53-509189791baa") // Lettuce
    },
    new RecipeTag {
    RecipeId = Guid.Parse("40ecea83-f36e-413e-a5e0-e4d7a9a4190a"),
    TagId = Guid.Parse("d319ba66-c185-4f7a-9a53-509189791baa") // Lettuce
    }
    //RecipeTag Nhan
    ];
}
