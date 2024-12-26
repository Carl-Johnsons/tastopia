using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIconUrlToTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Recipes_RecipeId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTag_Recipes_RecipeId",
                table: "RecipeTag");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeVote_Recipes_RecipeId",
                table: "RecipeVote");

            migrationBuilder.DropForeignKey(
                name: "FK_Step_Recipes_RecipeId",
                table: "Step");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBookmarkRecipe_Recipes_RecipeId",
                table: "UserBookmarkRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReportRecipe_Recipes_RecipeId",
                table: "UserReportRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes");

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("04801fc7-23f2-42bd-9b50-7010762b3f96"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("076d5723-b072-401e-9d7a-93089786f429"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("39eec6f7-1b77-4b17-a7a8-1a904988a6f6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("412dfd68-48a7-44dc-aa18-7ac8a0c1335a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("4217e5dc-33eb-4050-9b74-70ae27973108"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("51141a2a-a950-4e73-97f4-644358585a18"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6b2d5af6-c354-4c55-85de-c73da4e38974"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6cd9e21c-e91b-4f04-9a35-3513aaf5f97a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("710ef3c7-2790-49ba-919c-e7cb1fd672a5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("75db6aff-549d-48d2-a3dd-6c0e8929ef4b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("784e0b7f-2942-44a2-b398-97dbc2ec58bb"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c7b99ec3-347c-44cb-a6f4-546b3d2eee10"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c9564cc0-aa88-4d42-b5fc-14ee383ae5cd"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ccdf43eb-1591-47c7-acca-a36cc5d66f01"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e5671af0-3bfe-4482-b5f5-6bd27151a788"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e64bfce8-9614-45ef-a59a-20c0d94e2a87"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e76a3192-a5ee-4a40-9db2-f3d5b0e2bc6d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ee858298-bc0a-4b9b-bfa4-7bb73125884d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f71db38a-9789-46a4-8ab8-1492a9c75ea7"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("faa8e3a7-1166-4e8b-a142-62b1f7f32cc9"));

            migrationBuilder.RenameTable(
                name: "Recipes",
                newName: "Recipe");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Tag",
                newName: "IconUrl");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Tag",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe",
                column: "Id");

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
                columns: new[] { "Code", "CreatedAt", "IconUrl", "UpdatedAt" },
                values: new object[] { "MUSHROOM", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(136), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735188127/default_storage/tag/bgbyg81zcbdjpijcn8ep.png", new DateTime(2024, 12, 26, 4, 52, 0, 938, DateTimeKind.Utc).AddTicks(137) });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Recipe_RecipeId",
                table: "Comment",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTag_Recipe_RecipeId",
                table: "RecipeTag",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeVote_Recipe_RecipeId",
                table: "RecipeVote",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Recipe_RecipeId",
                table: "Step",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookmarkRecipe_Recipe_RecipeId",
                table: "UserBookmarkRecipe",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReportRecipe_Recipe_RecipeId",
                table: "UserReportRecipe",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Recipe_RecipeId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTag_Recipe_RecipeId",
                table: "RecipeTag");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeVote_Recipe_RecipeId",
                table: "RecipeVote");

            migrationBuilder.DropForeignKey(
                name: "FK_Step_Recipe_RecipeId",
                table: "Step");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBookmarkRecipe_Recipe_RecipeId",
                table: "UserBookmarkRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReportRecipe_Recipe_RecipeId",
                table: "UserReportRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe");

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

            migrationBuilder.RenameTable(
                name: "Recipe",
                newName: "Recipes");

            migrationBuilder.RenameColumn(
                name: "IconUrl",
                table: "Tag",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Tag",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"),
                column: "Ingredients",
                value: new List<string> { "1 Baguette", "50g Butter", "2 Garlic Cloves", "1 tbsp Parsley", "Salt" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
                column: "Ingredients",
                value: new List<string> { "200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
                column: "Ingredients",
                value: new List<string> { "1 Bell Pepper", "1 Carrot", "1 Broccoli Head", "2 tbsp Soy Sauce", "1 tbsp Olive Oil", "1 Garlic Clove" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"),
                column: "Ingredients",
                value: new List<string> { "2 Eggs", "2 tbsp Milk", "1 tbsp Butter", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("c8362fc3-5cff-4171-a78d-40613c748596"),
                column: "Ingredients",
                value: new List<string> { "4 Ripe Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" });

            migrationBuilder.InsertData(
                table: "Step",
                columns: new[] { "Id", "AttachedFiles", "Content", "CreatedAt", "OdinalNumber", "RecipeId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("04801fc7-23f2-42bd-9b50-7010762b3f96"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("076d5723-b072-401e-9d7a-93089786f429"), null, "Add vegetables and stir-fry for 5-7 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("39eec6f7-1b77-4b17-a7a8-1a904988a6f6"), null, "Blend the soup until smooth and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("412dfd68-48a7-44dc-aa18-7ac8a0c1335a"), null, "Mix softened butter with minced garlic and parsley.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4217e5dc-33eb-4050-9b74-70ae27973108"), null, "Crack the eggs into a bowl and whisk with milk, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("51141a2a-a950-4e73-97f4-644358585a18"), null, "Fry bacon until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6b2d5af6-c354-4c55-85de-c73da4e38974"), null, "Spread the mixture onto sliced baguette.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6cd9e21c-e91b-4f04-9a35-3513aaf5f97a"), null, "Add soy sauce and stir well.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("710ef3c7-2790-49ba-919c-e7cb1fd672a5"), null, "Add tomatoes and vegetable stock, then simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("75db6aff-549d-48d2-a3dd-6c0e8929ef4b"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("784e0b7f-2942-44a2-b398-97dbc2ec58bb"), null, "Mix eggs and grated parmesan in a bowl.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c7b99ec3-347c-44cb-a6f4-546b3d2eee10"), null, "Sauté onion and garlic in olive oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c9564cc0-aa88-4d42-b5fc-14ee383ae5cd"), null, "Heat olive oil in a wok or large pan.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ccdf43eb-1591-47c7-acca-a36cc5d66f01"), null, "Melt butter in a non-stick pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e5671af0-3bfe-4482-b5f5-6bd27151a788"), null, "Chop all vegetables into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e64bfce8-9614-45ef-a59a-20c0d94e2a87"), null, "Add the egg mixture and stir until creamy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e76a3192-a5ee-4a40-9db2-f3d5b0e2bc6d"), null, "Toss cooked spaghetti with bacon and remove from heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ee858298-bc0a-4b9b-bfa4-7bb73125884d"), null, "Bake in the oven for 10-12 minutes until golden.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f71db38a-9789-46a4-8ab8-1492a9c75ea7"), null, "Cook spaghetti in salted boiling water until al dente.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("faa8e3a7-1166-4e8b-a142-62b1f7f32cc9"), null, "Pour the egg mixture into the pan and gently stir until softly set.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("2bf7f026-e745-4bd9-8701-a9519742d0f7"),
                columns: new[] { "CreatedAt", "Status", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495"),
                columns: new[] { "CreatedAt", "Status", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8"),
                columns: new[] { "CreatedAt", "Status", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6"),
                columns: new[] { "CreatedAt", "Status", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"),
                columns: new[] { "CreatedAt", "Status", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e"),
                columns: new[] { "CreatedAt", "Status", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("92316e11-fd87-4c0f-aac7-bde4f19c2b38"),
                columns: new[] { "CreatedAt", "Status", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6"),
                columns: new[] { "CreatedAt", "Status", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("a7a1953d-027e-43b6-ad0d-d10312a3064d"),
                columns: new[] { "Code", "CreatedAt", "Status", "UpdatedAt" },
                values: new object[] { "MUSKROOM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e"),
                columns: new[] { "CreatedAt", "Status", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("df3f6301-3cae-480a-87da-c7b8f6150292"),
                columns: new[] { "CreatedAt", "Status", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3"),
                columns: new[] { "CreatedAt", "Status", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Recipes_RecipeId",
                table: "Comment",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTag_Recipes_RecipeId",
                table: "RecipeTag",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeVote_Recipes_RecipeId",
                table: "RecipeVote",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Recipes_RecipeId",
                table: "Step",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookmarkRecipe_Recipes_RecipeId",
                table: "UserBookmarkRecipe",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReportRecipe_Recipes_RecipeId",
                table: "UserReportRecipe",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
