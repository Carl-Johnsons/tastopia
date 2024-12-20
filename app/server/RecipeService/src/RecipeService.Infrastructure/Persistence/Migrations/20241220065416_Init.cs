using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Ingredients = table.Column<List<string>>(type: "text[]", nullable: false),
                    CookTime = table.Column<string>(type: "text", nullable: true),
                    Serves = table.Column<int>(type: "integer", nullable: true),
                    VoteDiff = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TotalView = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeVote",
                columns: table => new
                {
                    RecipeId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsUpvote = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeVote", x => new { x.RecipeId, x.UserId });
                    table.ForeignKey(
                        name: "FK_RecipeVote_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Step",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uuid", nullable: false),
                    OdinalNumber = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    AttachedFiles = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Step", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Step_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBookmarkRecipe",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookmarkRecipe", x => new { x.UserId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_UserBookmarkRecipe_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserReportRecipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReportRecipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReportRecipe_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.RecipeId, x.TagId });
                    table.ForeignKey(
                        name: "FK_RecipeTag_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentVote",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsUpvote = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentVote", x => new { x.CommentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CommentVote_Comment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserReportComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReportComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReportComment_Comment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "AuthorId", "CookTime", "CreatedAt", "Description", "ImageUrl", "Ingredients", "IsActive", "Serves", "Title", "TotalView", "UpdatedAt", "VoteDiff" },
                values: new object[,]
                {
                    { new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "15m", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A simple and delicious garlic bread recipe, perfect as a side dish.", "https://i.imgur.com/RpT3aRb.jpg", new List<string> { "1 Baguette", "50g Butter", "2 Garlic Cloves", "1 tbsp Parsley", "Salt" }, true, 2, "Garlic Bread", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "30m", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A creamy, cheesy pasta with bacon, eggs, and parmesan.", "https://i.imgur.com/le7gFC6.jpg", new List<string> { "200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper" }, true, 2, "Pasta Carbonara", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "20m", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A healthy stir-fry with various vegetables and soy sauce.", "https://i.imgur.com/aXVMMXA.jpg", new List<string> { "1 Bell Pepper", "1 Carrot", "1 Broccoli Head", "2 tbsp Soy Sauce", "1 tbsp Olive Oil", "1 Garlic Clove" }, true, 2, "Vegetable Stir-Fry", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "10m", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A quick and easy recipe for creamy scrambled eggs, perfect for breakfast.", "https://i.imgur.com/rxAzMjR.jpg", new List<string> { "2 Eggs", "2 tbsp Milk", "1 tbsp Butter", "Salt", "Pepper" }, true, 2, "Classic Scrambled Eggs", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "40m", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A comforting tomato soup made from fresh tomatoes and spices.", "https://i.imgur.com/SzhMVWs.jpg", new List<string> { "4 Ripe Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" }, true, 2, "Tomato Soup", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "Category", "Code", "CreatedAt", "ImageUrl", "IsActive", "Status", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { new Guid("2bf7f026-e745-4bd9-8701-a9519742d0f7"), "INGREDIENT", "EGG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://i.imgur.com/BAT5qyL.png", true, "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Egg" },
                    { new Guid("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495"), "INGREDIENT", "CHEESE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://i.imgur.com/feglS7k.jpg", true, "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cheese" },
                    { new Guid("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8"), "INGREDIENT", "SOY_SAUCE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://i.imgur.com/2QiWJWH.jpg", true, "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Soy Sauce" },
                    { new Guid("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6"), "INGREDIENT", "BROCCOLI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://i.imgur.com/8nDcffy.png", true, "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Broccoli" },
                    { new Guid("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"), "INGREDIENT", "BUTTER", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://i.imgur.com/Z8y4Hsr.jpg", true, "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Butter" },
                    { new Guid("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e"), "INGREDIENT", "GARLIC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://i.imgur.com/oLwdHvx.jpg", true, "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Garlic" },
                    { new Guid("92316e11-fd87-4c0f-aac7-bde4f19c2b38"), "INGREDIENT", "RICE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://i.imgur.com/C4nNmU1.png", true, "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rice" },
                    { new Guid("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6"), "INGREDIENT", "BACON", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://i.imgur.com/lyYgVRi.jpg", true, "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bacon" },
                    { new Guid("a7a1953d-027e-43b6-ad0d-d10312a3064d"), "INGREDIENT", "MUSKROOM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://i.imgur.com/m8wBuYO.png", true, "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mushroom" },
                    { new Guid("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e"), "INGREDIENT", "CARROT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://i.imgur.com/DZEq7TK.jpg", true, "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Carrot" },
                    { new Guid("df3f6301-3cae-480a-87da-c7b8f6150292"), "INGREDIENT", "TOMATO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://i.imgur.com/3NovRt2.png", true, "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tomato" },
                    { new Guid("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3"), "INGREDIENT", "MILK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://i.imgur.com/Rk3MwdQ.jpg", true, "Pending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Milk" }
                });

            migrationBuilder.InsertData(
                table: "RecipeTag",
                columns: new[] { "RecipeId", "TagId" },
                values: new object[,]
                {
                    { new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new Guid("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1") },
                    { new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new Guid("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e") },
                    { new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new Guid("2bf7f026-e745-4bd9-8701-a9519742d0f7") },
                    { new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new Guid("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495") },
                    { new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new Guid("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6") },
                    { new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new Guid("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8") },
                    { new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new Guid("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6") },
                    { new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new Guid("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e") },
                    { new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new Guid("2bf7f026-e745-4bd9-8701-a9519742d0f7") },
                    { new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new Guid("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1") },
                    { new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new Guid("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3") },
                    { new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new Guid("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e") },
                    { new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new Guid("df3f6301-3cae-480a-87da-c7b8f6150292") }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Comment_RecipeId",
                table: "Comment",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_TagId",
                table: "RecipeTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Step_RecipeId",
                table: "Step",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookmarkRecipe_RecipeId",
                table: "UserBookmarkRecipe",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReportComment_CommentId",
                table: "UserReportComment",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReportRecipe_RecipeId",
                table: "UserReportRecipe",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentVote");

            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "RecipeVote");

            migrationBuilder.DropTable(
                name: "Step");

            migrationBuilder.DropTable(
                name: "UserBookmarkRecipe");

            migrationBuilder.DropTable(
                name: "UserReportComment");

            migrationBuilder.DropTable(
                name: "UserReportRecipe");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
