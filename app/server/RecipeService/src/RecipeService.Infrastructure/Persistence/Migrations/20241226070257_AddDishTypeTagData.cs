using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDishTypeTagData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("06f36712-d385-4b21-80e5-9428fa2f89bf"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0d79f1bf-b95f-4369-85d7-e35e9e8853e2"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0f7a55f2-ba2c-4f9b-8f95-c39f98863d10"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1bdc04d5-46b5-4f32-b3b1-d01c64ff094f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1d719f9a-db24-4188-8757-18c06c483c89"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("26e67ae3-8aad-443c-a700-61afcfc63db0"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("39e83a4c-5168-4a8d-ac80-b5e2bc77a976"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("453f6a84-06d6-4270-b8ea-27210e1efb04"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("4b74d62f-2412-44d2-9489-379bd40acf05"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("62415611-56ed-41c2-a39b-02a75230541e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6cafce92-8dcb-4d5e-8f66-190e09f618fc"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("74b92257-1486-45f5-9cc5-779a7b5e8ab2"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8dd3c0f3-7d21-4dc4-8fbc-eecf688f0dce"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8f81809b-712f-41d3-a0ee-d3b919ea451b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("93d4e2fe-89ac-49de-a1d7-b9c4a275ca5c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9d224282-cf84-48f8-81d7-2f31148e03da"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a2fe8cf3-3aed-4201-93ea-9908ca4cd9ee"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b98f90b7-8c14-4783-b8a0-55b4790f77ab"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c255c6fb-dac7-47e5-9776-ab8b2d5c0ab6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ea7f6a32-9d16-4cc7-b468-24077e185b3f"));

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Tag");

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"),
                column: "Ingredients",
                value: new List<string> { "1 Baguette", "50g Butter", "2 Garlic Cloves", "1 tbsp Parsley", "Salt" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
                column: "Ingredients",
                value: new List<string> { "200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
                column: "Ingredients",
                value: new List<string> { "1 Bell Pepper", "1 Carrot", "1 Broccoli Head", "2 tbsp Soy Sauce", "1 tbsp Olive Oil", "1 Garlic Clove" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"),
                column: "Ingredients",
                value: new List<string> { "2 Eggs", "2 tbsp Milk", "1 tbsp Butter", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("c8362fc3-5cff-4171-a78d-40613c748596"),
                column: "Ingredients",
                value: new List<string> { "4 Ripe Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" });

            migrationBuilder.InsertData(
                table: "Step",
                columns: new[] { "Id", "AttachedFiles", "Content", "CreatedAt", "OdinalNumber", "RecipeId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0172bcaa-5e34-4f73-9ae1-f3c3515e4ec0"), null, "Bake in the oven for 10-12 minutes until golden.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("199f6f87-2d03-438b-9640-500a5dc0f01b"), null, "Mix eggs and grated parmesan in a bowl.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2047fa61-fda4-4f16-bfbc-5226fc858b3e"), null, "Sauté onion and garlic in olive oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("28933c6b-7d7f-4c90-998a-263b10c89565"), null, "Add the egg mixture and stir until creamy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("29e31599-38b9-4e45-8d58-71ddbe5f190e"), null, "Crack the eggs into a bowl and whisk with milk, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3aa99604-c1b1-4eea-9015-c267334ae469"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("40185714-cc96-4f7a-bdf0-fb13dc1a538e"), null, "Toss cooked spaghetti with bacon and remove from heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5b367030-a0bd-42d6-80dc-88759546e501"), null, "Blend the soup until smooth and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("65025b44-cc8c-4cc1-8e9d-3054273f67a3"), null, "Spread the mixture onto sliced baguette.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("662b7860-8a1e-4b4e-a5d3-7b9daf4b3598"), null, "Add tomatoes and vegetable stock, then simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("988af948-96f9-41f3-a478-4d9af37ca14d"), null, "Melt butter in a non-stick pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9f57bc77-07e5-4220-af33-bad51fbf80d5"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a307aca3-f0f7-40f3-8769-5b58c14dd3d2"), null, "Cook spaghetti in salted boiling water until al dente.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ad1b1228-8545-420d-b78d-cc1d987a8bf9"), null, "Add soy sauce and stir well.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b69128bc-0885-49a8-87a3-7a8b13655c4d"), null, "Chop all vegetables into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b92f4f54-ff62-40c3-b225-03c2ca90415d"), null, "Fry bacon until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e0005e71-dad1-4ce3-a24b-aa7f93d07fb4"), null, "Heat olive oil in a wok or large pan.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e08ed509-ce1f-42ff-830d-6ab97634d8f1"), null, "Pour the egg mixture into the pan and gently stir until softly set.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f0df186a-906e-4a33-93ee-9afef235aa62"), null, "Mix softened butter with minced garlic and parsley.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f39432c2-9af4-4d58-af3e-b17660cceb64"), null, "Add vegetables and stir-fry for 5-7 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("2bf7f026-e745-4bd9-8701-a9519742d0f7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7238), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7238) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7253), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7254) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7265), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7266) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7263), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7263) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7251), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7251) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7258), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7258) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("92316e11-fd87-4c0f-aac7-bde4f19c2b38"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7241), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7242) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7256), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7256) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("a7a1953d-027e-43b6-ad0d-d10312a3064d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7246), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7246) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7260), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7261) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("df3f6301-3cae-480a-87da-c7b8f6150292"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7226), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7236) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7248), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7249) });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "Category", "Code", "CreatedAt", "ImageUrl", "IsActive", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { new Guid("042dd338-3338-45a1-96c6-a1e2d0ca71ff"), "DISHTYPE", "SEAFOOD", new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7279), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196329/default_storage/tag/dishtype/jhxdroetbjq9f57cixwj.png", true, new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7279), "Seafood" },
                    { new Guid("6c91b894-d6dd-4c9b-a106-bfd029ce9e16"), "DISHTYPE", "NOODLES", new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7270), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196327/default_storage/tag/dishtype/ingre9ficyzq0iitiwmd.png", true, new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7271), "Noodles" },
                    { new Guid("71676633-493e-46c5-86a0-21773f196035"), "DISHTYPE", "ALL", new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7268), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196328/default_storage/tag/dishtype/hivalxshuvx5nnlfp5t1.png", true, new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7268), "All" },
                    { new Guid("d8b74fc2-f848-41af-a53f-20170aa453cd"), "DISHTYPE", "BBQ", new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7275), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196328/default_storage/tag/dishtype/zl1f9g0dxxbtw2f4jiel.png", true, new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7276), "BBQ" },
                    { new Guid("de3698bf-ad8b-4cc6-8ed0-662bd7eca486"), "DISHTYPE", "SPICE", new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7273), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196323/default_storage/tag/dishtype/frgtox8kb4edsvmutmez.png", true, new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7273), "Spice" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0172bcaa-5e34-4f73-9ae1-f3c3515e4ec0"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("199f6f87-2d03-438b-9640-500a5dc0f01b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("2047fa61-fda4-4f16-bfbc-5226fc858b3e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("28933c6b-7d7f-4c90-998a-263b10c89565"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("29e31599-38b9-4e45-8d58-71ddbe5f190e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("3aa99604-c1b1-4eea-9015-c267334ae469"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("40185714-cc96-4f7a-bdf0-fb13dc1a538e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("5b367030-a0bd-42d6-80dc-88759546e501"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("65025b44-cc8c-4cc1-8e9d-3054273f67a3"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("662b7860-8a1e-4b4e-a5d3-7b9daf4b3598"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("988af948-96f9-41f3-a478-4d9af37ca14d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9f57bc77-07e5-4220-af33-bad51fbf80d5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a307aca3-f0f7-40f3-8769-5b58c14dd3d2"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ad1b1228-8545-420d-b78d-cc1d987a8bf9"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b69128bc-0885-49a8-87a3-7a8b13655c4d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b92f4f54-ff62-40c3-b225-03c2ca90415d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e0005e71-dad1-4ce3-a24b-aa7f93d07fb4"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e08ed509-ce1f-42ff-830d-6ab97634d8f1"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f0df186a-906e-4a33-93ee-9afef235aa62"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f39432c2-9af4-4d58-af3e-b17660cceb64"));

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("042dd338-3338-45a1-96c6-a1e2d0ca71ff"));

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6c91b894-d6dd-4c9b-a106-bfd029ce9e16"));

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("71676633-493e-46c5-86a0-21773f196035"));

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("d8b74fc2-f848-41af-a53f-20170aa453cd"));

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("de3698bf-ad8b-4cc6-8ed0-662bd7eca486"));

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Tag",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"),
                column: "Ingredients",
                value: new List<string> { "1 Baguette", "50g Butter", "2 Garlic Cloves", "1 tbsp Parsley", "Salt" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
                column: "Ingredients",
                value: new List<string> { "200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
                column: "Ingredients",
                value: new List<string> { "1 Bell Pepper", "1 Carrot", "1 Broccoli Head", "2 tbsp Soy Sauce", "1 tbsp Olive Oil", "1 Garlic Clove" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"),
                column: "Ingredients",
                value: new List<string> { "2 Eggs", "2 tbsp Milk", "1 tbsp Butter", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("c8362fc3-5cff-4171-a78d-40613c748596"),
                column: "Ingredients",
                value: new List<string> { "4 Ripe Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" });

            migrationBuilder.InsertData(
                table: "Step",
                columns: new[] { "Id", "AttachedFiles", "Content", "CreatedAt", "OdinalNumber", "RecipeId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("06f36712-d385-4b21-80e5-9428fa2f89bf"), null, "Pour the egg mixture into the pan and gently stir until softly set.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0d79f1bf-b95f-4369-85d7-e35e9e8853e2"), null, "Add tomatoes and vegetable stock, then simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0f7a55f2-ba2c-4f9b-8f95-c39f98863d10"), null, "Add vegetables and stir-fry for 5-7 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1bdc04d5-46b5-4f32-b3b1-d01c64ff094f"), null, "Chop all vegetables into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1d719f9a-db24-4188-8757-18c06c483c89"), null, "Mix softened butter with minced garlic and parsley.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("26e67ae3-8aad-443c-a700-61afcfc63db0"), null, "Crack the eggs into a bowl and whisk with milk, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("39e83a4c-5168-4a8d-ac80-b5e2bc77a976"), null, "Blend the soup until smooth and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("453f6a84-06d6-4270-b8ea-27210e1efb04"), null, "Heat olive oil in a wok or large pan.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4b74d62f-2412-44d2-9489-379bd40acf05"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("62415611-56ed-41c2-a39b-02a75230541e"), null, "Mix eggs and grated parmesan in a bowl.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6cafce92-8dcb-4d5e-8f66-190e09f618fc"), null, "Sauté onion and garlic in olive oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("74b92257-1486-45f5-9cc5-779a7b5e8ab2"), null, "Bake in the oven for 10-12 minutes until golden.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8dd3c0f3-7d21-4dc4-8fbc-eecf688f0dce"), null, "Cook spaghetti in salted boiling water until al dente.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8f81809b-712f-41d3-a0ee-d3b919ea451b"), null, "Fry bacon until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("93d4e2fe-89ac-49de-a1d7-b9c4a275ca5c"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9d224282-cf84-48f8-81d7-2f31148e03da"), null, "Spread the mixture onto sliced baguette.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a2fe8cf3-3aed-4201-93ea-9908ca4cd9ee"), null, "Melt butter in a non-stick pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b98f90b7-8c14-4783-b8a0-55b4790f77ab"), null, "Toss cooked spaghetti with bacon and remove from heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c255c6fb-dac7-47e5-9776-ab8b2d5c0ab6"), null, "Add the egg mixture and stir until creamy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ea7f6a32-9d16-4cc7-b468-24077e185b3f"), null, "Add soy sauce and stir well.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("2bf7f026-e745-4bd9-8701-a9519742d0f7"),
                columns: new[] { "CreatedAt", "IconUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(129), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188128/default_storage/tag/gcogeajch6fpvqohribk.png", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(129) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495"),
                columns: new[] { "CreatedAt", "IconUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(152), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188128/default_storage/tag/pxx8vnkcs3ibwqubvpa1.png", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(153) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8"),
                columns: new[] { "CreatedAt", "IconUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(175), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188127/default_storage/tag/xc8zoiy2brchcuqgyxhq.png", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(175) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6"),
                columns: new[] { "CreatedAt", "IconUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(171), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188127/default_storage/tag/dcinjpqufvrlanwqyj4g.png", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(172) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"),
                columns: new[] { "CreatedAt", "IconUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(143), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188128/default_storage/tag/te5yyfu8xlldcxygov08.png", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(144) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e"),
                columns: new[] { "CreatedAt", "IconUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(164), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188127/default_storage/tag/eyy3xy151cyv660ezq1w.png", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(165) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("92316e11-fd87-4c0f-aac7-bde4f19c2b38"),
                columns: new[] { "CreatedAt", "IconUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(133), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188128/default_storage/tag/jendg3sl9ptvhgow2jrx.png", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(133) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6"),
                columns: new[] { "CreatedAt", "IconUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(158), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188127/default_storage/tag/cm7vdqacstnodzodzx0o.png", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(158) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("a7a1953d-027e-43b6-ad0d-d10312a3064d"),
                columns: new[] { "CreatedAt", "IconUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(136), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188127/default_storage/tag/bgbyg81zcbdjpijcn8ep.png", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(137) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e"),
                columns: new[] { "CreatedAt", "IconUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(168), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188127/default_storage/tag/bqnsia9cjejk24bma2sl.png", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(168) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("df3f6301-3cae-480a-87da-c7b8f6150292"),
                columns: new[] { "CreatedAt", "IconUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(115), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188128/default_storage/tag/pkain4xbzohdhv7aeeng.png", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(126) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3"),
                columns: new[] { "CreatedAt", "IconUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(140), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188128/default_storage/tag/mepnrydhtvbsjqw8c0ht.png", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(140) });
        }
    }
}
