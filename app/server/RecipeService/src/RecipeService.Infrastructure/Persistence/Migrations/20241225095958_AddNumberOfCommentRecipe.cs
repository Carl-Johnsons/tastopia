using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberOfCommentRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("08be2caa-3b2a-459c-a444-7b56c4730ae6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0a1740a0-04c1-4c19-8d55-0d656a3d1f9f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("12741ec7-bd19-41a3-a660-2432bc67e256"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1408f3a7-ae38-4daf-8743-fd2fae904fed"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("348b25ac-0493-4359-912c-a562612151ea"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("3cfbf6ae-9039-452e-b678-ad59432e8119"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("41b53be9-5734-4645-a133-db2b82c40fb4"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("449141a8-a1e8-442e-aefd-318b7ce5cf65"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("4fdce893-7fa1-474c-9bc3-a795816d1473"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("56aed4bf-50a9-4f38-9dbb-ced3a6774c9b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("619206a6-c917-48a2-8b6f-1cb16976b5bb"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6d027f42-71e4-401e-b16f-17bf9d780608"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7cb53d98-5e07-4a3d-87ac-258f8d295f27"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("81ce7606-175b-4575-a463-ebaa8650182a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("84e41e99-9d9d-40b5-ae16-83e52750f9c9"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("cb287db2-5ffe-4b2b-b315-4ae5388572cf"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("cd4da405-7aec-4528-8d6b-158c4dda7389"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e61f710c-1919-4161-8c6f-7c5364ebaa8f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f3ce6a21-ec8e-4277-a880-b52223f21c11"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f6b3ab19-0aeb-44fc-8c17-f98238af5791"));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "UserReportRecipe",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "UserReportRecipe",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "UserReportComment",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "UserReportComment",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Step",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "VoteDiff",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Recipes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Recipes",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfComment",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comment",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"),
                columns: new[] { "CreatedAt", "Ingredients", "NumberOfComment", "UpdatedAt", "VoteDiff" },
                values: new object[] { new DateTime(2023, 9, 30, 18, 9, 19, 0, DateTimeKind.Utc), new List<string> { "1 Baguette", "50g Butter", "2 Garlic Cloves", "1 tbsp Parsley", "Salt" }, 0, new DateTime(2023, 9, 30, 18, 9, 19, 0, DateTimeKind.Utc), 0 });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
                columns: new[] { "CreatedAt", "Ingredients", "NumberOfComment", "UpdatedAt", "VoteDiff" },
                values: new object[] { new DateTime(2023, 5, 31, 12, 6, 57, 0, DateTimeKind.Utc), new List<string> { "200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper" }, 0, new DateTime(2023, 5, 31, 12, 6, 57, 0, DateTimeKind.Utc), 0 });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
                columns: new[] { "CreatedAt", "Ingredients", "NumberOfComment", "UpdatedAt", "VoteDiff" },
                values: new object[] { new DateTime(2023, 2, 18, 23, 28, 9, 0, DateTimeKind.Utc), new List<string> { "1 Bell Pepper", "1 Carrot", "1 Broccoli Head", "2 tbsp Soy Sauce", "1 tbsp Olive Oil", "1 Garlic Clove" }, 0, new DateTime(2023, 2, 18, 23, 28, 9, 0, DateTimeKind.Utc), 0 });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"),
                columns: new[] { "CreatedAt", "Ingredients", "NumberOfComment", "UpdatedAt", "VoteDiff" },
                values: new object[] { new DateTime(2024, 7, 28, 22, 11, 18, 0, DateTimeKind.Utc), new List<string> { "2 Eggs", "2 tbsp Milk", "1 tbsp Butter", "Salt", "Pepper" }, 0, new DateTime(2024, 7, 28, 22, 11, 18, 0, DateTimeKind.Utc), 0 });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("c8362fc3-5cff-4171-a78d-40613c748596"),
                columns: new[] { "CreatedAt", "Ingredients", "NumberOfComment", "UpdatedAt", "VoteDiff" },
                values: new object[] { new DateTime(2023, 12, 24, 19, 1, 34, 0, DateTimeKind.Utc), new List<string> { "4 Ripe Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" }, 0, new DateTime(2023, 12, 24, 19, 1, 34, 0, DateTimeKind.Utc), 0 });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "NumberOfComment",
                table: "Recipes");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "UserReportRecipe",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "UserReportRecipe",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "UserReportComment",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "UserReportComment",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Step",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<int>(
                name: "VoteDiff",
                table: "Recipes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Recipes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Recipes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comment",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"),
                columns: new[] { "CreatedAt", "Ingredients", "UpdatedAt", "VoteDiff" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new List<string> { "1 Baguette", "50g Butter", "2 Garlic Cloves", "1 tbsp Parsley", "Salt" }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
                columns: new[] { "CreatedAt", "Ingredients", "UpdatedAt", "VoteDiff" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new List<string> { "200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper" }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
                columns: new[] { "CreatedAt", "Ingredients", "UpdatedAt", "VoteDiff" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new List<string> { "1 Bell Pepper", "1 Carrot", "1 Broccoli Head", "2 tbsp Soy Sauce", "1 tbsp Olive Oil", "1 Garlic Clove" }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"),
                columns: new[] { "CreatedAt", "Ingredients", "UpdatedAt", "VoteDiff" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new List<string> { "2 Eggs", "2 tbsp Milk", "1 tbsp Butter", "Salt", "Pepper" }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("c8362fc3-5cff-4171-a78d-40613c748596"),
                columns: new[] { "CreatedAt", "Ingredients", "UpdatedAt", "VoteDiff" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new List<string> { "4 Ripe Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null });

            migrationBuilder.InsertData(
                table: "Step",
                columns: new[] { "Id", "AttachedFiles", "Content", "CreatedAt", "OdinalNumber", "RecipeId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("08be2caa-3b2a-459c-a444-7b56c4730ae6"), null, "Fry bacon until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0a1740a0-04c1-4c19-8d55-0d656a3d1f9f"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("12741ec7-bd19-41a3-a660-2432bc67e256"), null, "Sauté onion and garlic in olive oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1408f3a7-ae38-4daf-8743-fd2fae904fed"), null, "Mix eggs and grated parmesan in a bowl.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("348b25ac-0493-4359-912c-a562612151ea"), null, "Bake in the oven for 10-12 minutes until golden.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3cfbf6ae-9039-452e-b678-ad59432e8119"), null, "Melt butter in a non-stick pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("41b53be9-5734-4645-a133-db2b82c40fb4"), null, "Mix softened butter with minced garlic and parsley.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("449141a8-a1e8-442e-aefd-318b7ce5cf65"), null, "Blend the soup until smooth and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4fdce893-7fa1-474c-9bc3-a795816d1473"), null, "Add vegetables and stir-fry for 5-7 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("56aed4bf-50a9-4f38-9dbb-ced3a6774c9b"), null, "Add soy sauce and stir well.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("619206a6-c917-48a2-8b6f-1cb16976b5bb"), null, "Toss cooked spaghetti with bacon and remove from heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6d027f42-71e4-401e-b16f-17bf9d780608"), null, "Add tomatoes and vegetable stock, then simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7cb53d98-5e07-4a3d-87ac-258f8d295f27"), null, "Heat olive oil in a wok or large pan.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("81ce7606-175b-4575-a463-ebaa8650182a"), null, "Crack the eggs into a bowl and whisk with milk, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("84e41e99-9d9d-40b5-ae16-83e52750f9c9"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cb287db2-5ffe-4b2b-b315-4ae5388572cf"), null, "Pour the egg mixture into the pan and gently stir until softly set.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cd4da405-7aec-4528-8d6b-158c4dda7389"), null, "Chop all vegetables into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e61f710c-1919-4161-8c6f-7c5364ebaa8f"), null, "Cook spaghetti in salted boiling water until al dente.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f3ce6a21-ec8e-4277-a880-b52223f21c11"), null, "Spread the mixture onto sliced baguette.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f6b3ab19-0aeb-44fc-8c17-f98238af5791"), null, "Add the egg mixture and stir until creamy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }
    }
}
