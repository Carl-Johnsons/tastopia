using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAttachedFilesToStringArray : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "AttachedFiles",
                table: "Step");

            migrationBuilder.AddColumn<List<string>>(
                name: "AttachedImageUrls",
                table: "Step",
                type: "text[]",
                nullable: true);

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
                columns: new[] { "Id", "AttachedImageUrls", "Content", "CreatedAt", "OdinalNumber", "RecipeId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("21bedadf-1a37-4e53-a3b9-0a885ee60601"), null, "Spread the mixture onto sliced baguette.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("274f2842-a3e5-4a32-a8a5-5211dcab2912"), null, "Add the egg mixture and stir until creamy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2bd8dfb4-0ad0-41f6-b6f3-848c74138d25"), null, "Melt butter in a non-stick pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2df28722-ac4f-4965-b90c-c098131589f0"), null, "Add vegetables and stir-fry for 5-7 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("35483d6c-6c63-4603-87a2-e0111327e49b"), null, "Add soy sauce and stir well.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("48e5469b-cbfd-493a-8f97-87ccdeb582a4"), null, "Bake in the oven for 10-12 minutes until golden.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4d2f7874-73c4-41c0-aac4-214431825d73"), null, "Sauté onion and garlic in olive oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55559bf3-f134-427e-8552-99bd6710e5b3"), null, "Mix eggs and grated parmesan in a bowl.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5f1944af-04fc-4124-866f-c1685ef991c0"), null, "Heat olive oil in a wok or large pan.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5f286099-716e-4d82-91e1-d079d44d749e"), null, "Blend the soup until smooth and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("60c692b6-5cfd-45cc-ae02-9e9dd7fe3ca7"), null, "Add tomatoes and vegetable stock, then simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("91e558fa-5c9f-4d51-8342-0e6555e19a35"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("98a5f870-2f1b-436a-8d40-9a0cd451ecca"), null, "Toss cooked spaghetti with bacon and remove from heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a3f72e82-2025-44a1-9b74-1bc3999dd65b"), null, "Fry bacon until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a75ef94e-b0a9-41f1-87b6-004f2d052463"), null, "Crack the eggs into a bowl and whisk with milk, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ba1c4554-f0f4-4940-808f-bdd38adb5a39"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c042ed72-eee4-46cf-a58a-3cd4599e3060"), null, "Mix softened butter with minced garlic and parsley.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cb8f9f6d-79de-4012-a5ff-f5e270ca279a"), null, "Cook spaghetti in salted boiling water until al dente.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("df106c03-8e82-43a2-ae64-561164dc889f"), null, "Chop all vegetables into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e15a2a39-e321-48b8-97b2-c9872360daf8"), null, "Pour the egg mixture into the pan and gently stir until softly set.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("042dd338-3338-45a1-96c6-a1e2d0ca71ff"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7208), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7209) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("2bf7f026-e745-4bd9-8701-a9519742d0f7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7166), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7166) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7179), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7180) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7192), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7192) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7189), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7190) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6c91b894-d6dd-4c9b-a106-bfd029ce9e16"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7197), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7197) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7177), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7177) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("71676633-493e-46c5-86a0-21773f196035"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7194), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7195) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7184), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7185) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("92316e11-fd87-4c0f-aac7-bde4f19c2b38"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7169), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7170) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7182), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7182) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("a7a1953d-027e-43b6-ad0d-d10312a3064d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7172), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7172) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7187), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7187) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("d8b74fc2-f848-41af-a53f-20170aa453cd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7202), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7202) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("de3698bf-ad8b-4cc6-8ed0-662bd7eca486"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7199), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7200) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("df3f6301-3cae-480a-87da-c7b8f6150292"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7152), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7164) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7174), new DateTime(2024, 12, 26, 8, 28, 11, 795, DateTimeKind.Utc).AddTicks(7175) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("21bedadf-1a37-4e53-a3b9-0a885ee60601"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("274f2842-a3e5-4a32-a8a5-5211dcab2912"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("2bd8dfb4-0ad0-41f6-b6f3-848c74138d25"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("2df28722-ac4f-4965-b90c-c098131589f0"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("35483d6c-6c63-4603-87a2-e0111327e49b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("48e5469b-cbfd-493a-8f97-87ccdeb582a4"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("4d2f7874-73c4-41c0-aac4-214431825d73"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("55559bf3-f134-427e-8552-99bd6710e5b3"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("5f1944af-04fc-4124-866f-c1685ef991c0"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("5f286099-716e-4d82-91e1-d079d44d749e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("60c692b6-5cfd-45cc-ae02-9e9dd7fe3ca7"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("91e558fa-5c9f-4d51-8342-0e6555e19a35"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("98a5f870-2f1b-436a-8d40-9a0cd451ecca"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a3f72e82-2025-44a1-9b74-1bc3999dd65b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a75ef94e-b0a9-41f1-87b6-004f2d052463"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ba1c4554-f0f4-4940-808f-bdd38adb5a39"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c042ed72-eee4-46cf-a58a-3cd4599e3060"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("cb8f9f6d-79de-4012-a5ff-f5e270ca279a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("df106c03-8e82-43a2-ae64-561164dc889f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e15a2a39-e321-48b8-97b2-c9872360daf8"));

            migrationBuilder.DropColumn(
                name: "AttachedImageUrls",
                table: "Step");

            migrationBuilder.AddColumn<string>(
                name: "AttachedFiles",
                table: "Step",
                type: "text",
                nullable: true);

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
                keyValue: new Guid("042dd338-3338-45a1-96c6-a1e2d0ca71ff"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7279), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7279) });

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
                keyValue: new Guid("6c91b894-d6dd-4c9b-a106-bfd029ce9e16"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7270), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7271) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7251), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7251) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("71676633-493e-46c5-86a0-21773f196035"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7268), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7268) });

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
                keyValue: new Guid("d8b74fc2-f848-41af-a53f-20170aa453cd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7275), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7276) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("de3698bf-ad8b-4cc6-8ed0-662bd7eca486"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7273), new DateTime(2024, 12, 26, 7, 2, 56, 911, DateTimeKind.Utc).AddTicks(7273) });

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
        }
    }
}
