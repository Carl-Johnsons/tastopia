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
                name: "Recipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Ingredients = table.Column<List<string>>(type: "text[]", nullable: false),
                    CookTime = table.Column<string>(type: "text", nullable: true),
                    Serves = table.Column<int>(type: "integer", nullable: true),
                    VoteDiff = table.Column<int>(type: "integer", nullable: false),
                    NumberOfComment = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TotalView = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
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
                    Content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
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
                        name: "FK_Comment_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
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
                        name: "FK_RecipeVote_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
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
                    Content = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    AttachedImageUrls = table.Column<List<string>>(type: "text[]", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Step", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Step_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
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
                        name: "FK_UserBookmarkRecipe_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
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
                    Reason = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReportRecipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReportRecipe_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
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
                        name: "FK_RecipeTag_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
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
                    Reason = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
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
                table: "Recipe",
                columns: new[] { "Id", "AuthorId", "CookTime", "CreatedAt", "Description", "ImageUrl", "Ingredients", "IsActive", "NumberOfComment", "Serves", "Title", "TotalView", "UpdatedAt", "VoteDiff" },
                values: new object[,]
                {
                    { new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "15m", new DateTime(2023, 9, 30, 18, 9, 19, 0, DateTimeKind.Utc), "A simple and delicious garlic bread recipe, perfect as a side dish.", "https://i.imgur.com/RpT3aRb.jpg", new List<string> { "1 Baguette", "50g Butter", "2 Garlic Cloves", "1 tbsp Parsley", "Salt" }, true, 0, 2, "Garlic Bread", 0, new DateTime(2023, 9, 30, 18, 9, 19, 0, DateTimeKind.Utc), 0 },
                    { new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "30m", new DateTime(2023, 5, 31, 12, 6, 57, 0, DateTimeKind.Utc), "A creamy, cheesy pasta with bacon, eggs, and parmesan.", "https://i.imgur.com/le7gFC6.jpg", new List<string> { "200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper" }, true, 0, 2, "Pasta Carbonara", 0, new DateTime(2023, 5, 31, 12, 6, 57, 0, DateTimeKind.Utc), 0 },
                    { new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "20m", new DateTime(2023, 2, 18, 23, 28, 9, 0, DateTimeKind.Utc), "A healthy stir-fry with various vegetables and soy sauce.", "https://i.imgur.com/aXVMMXA.jpg", new List<string> { "1 Bell Pepper", "1 Carrot", "1 Broccoli Head", "2 tbsp Soy Sauce", "1 tbsp Olive Oil", "1 Garlic Clove" }, true, 0, 2, "Vegetable Stir-Fry", 0, new DateTime(2023, 2, 18, 23, 28, 9, 0, DateTimeKind.Utc), 0 },
                    { new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "40m", new DateTime(2024, 12, 8, 7, 0, 0, 0, DateTimeKind.Utc), "A warm and healthy carrot soup.", "https://www.allrecipes.com/thmb/9_1D87q39_oirnZ9CXlmllrd9Fc=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/Simple-Carrot-Soup-2000-d2312230d4454f53baef3fa78f22e7d7.jpg", new List<string> { "4 Carrots", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" }, true, 0, 4, "Carrot Soup", 0, new DateTime(2024, 12, 8, 7, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "45m", new DateTime(2024, 12, 4, 3, 0, 0, 0, DateTimeKind.Utc), "A hearty casserole with broccoli and melted cheese.", "https://www.homemadeinterest.com/wp-content/uploads/2022/09/Broccoli-Cheese-Casserole_-sq-1.jpg", new List<string> { "1 Broccoli Head", "200g Cheese", "1 Onion", "2 tbsp Butter", "Salt", "Pepper" }, true, 0, 4, "Broccoli and Cheese Casserole", 0, new DateTime(2024, 12, 4, 3, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "5m", new DateTime(2024, 12, 15, 14, 0, 0, 0, DateTimeKind.Utc), "A creamy and refreshing milkshake.", "https://www.allrecipes.com/thmb/uzxCGTc-5WCUZnZ7BUcYcmWKxjo=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/AR-48974-vanilla-milkshake-hero-4x3-c815295c714f41f6b17b104e7403a53b.jpg", new List<string> { "2 cups Milk", "100g Ice Cream", "1 tbsp Sugar" }, true, 0, 2, "Milkshake", 0, new DateTime(2024, 12, 15, 14, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "30m", new DateTime(2024, 12, 7, 6, 0, 0, 0, DateTimeKind.Utc), "A fresh and fragrant tomato basil soup.", "https://thecozyapron.com/wp-content/uploads/2012/02/tomato-basil-soup_thecozyapron_1.jpg", new List<string> { "4 Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Basil", "Salt", "Pepper" }, true, 0, 2, "Tomato Basil Soup", 0, new DateTime(2024, 12, 7, 6, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "15m", new DateTime(2024, 12, 6, 5, 0, 0, 0, DateTimeKind.Utc), "Shrimp sautéed in a rich garlic butter sauce.", "https://www.jocooks.com/wp-content/uploads/2021/09/garlic-butter-shrimp-1-10.jpg", new List<string> { "200g Shrimp", "2 Garlic Cloves", "50g Butter", "Salt", "Pepper", "Parsley" }, true, 0, 2, "Garlic Butter Shrimp", 0, new DateTime(2024, 12, 6, 5, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "30m", new DateTime(2024, 12, 11, 10, 0, 0, 0, DateTimeKind.Utc), "A creamy pasta carbonara with crispy bacon.", "https://img.bestrecipes.com.au/_bHk3l7K/br/2016/07/asset-jpg-506607-1.jpg", new List<string> { "200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper" }, true, 0, 2, "Bacon Carbonara", 0, new DateTime(2024, 12, 11, 10, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "20m", new DateTime(2024, 12, 3, 2, 0, 0, 0, DateTimeKind.Utc), "A quick and tasty egg fried rice recipe.", "https://www.mygingergarlickitchen.com/wp-content/rich-markup-images/4x3/4x3-indian-style-triple-egg-fried-rice-easy-egg-fried-rice-video-recipe.jpg", new List<string> { "1 cup Rice", "2 Eggs", "1 Carrot", "Soy Sauce", "2 tbsp Olive Oil", "Garlic" }, true, 0, 2, "Egg Fried Rice", 0, new DateTime(2024, 12, 3, 2, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "5m", new DateTime(2024, 12, 1, 1, 0, 0, 0, DateTimeKind.Utc), "A simple and delicious cheese omelette.", "https://www.emborg.com/app/uploads/2023/07/1200x900px_Easy_Cheese_Omelette.png", new List<string> { "2 Eggs", "50g Cheese", "Butter", "Salt", "Pepper" }, true, 0, 2, "Cheese Omelette", 0, new DateTime(2024, 12, 1, 1, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "30m", new DateTime(2024, 12, 9, 8, 0, 0, 0, DateTimeKind.Utc), "A flavorful rice pilaf with spices and vegetables.", "https://i2.wp.com/lifemadesimplebakes.com/wp-content/uploads/2021/02/perfect-rice-pilaf-square-1200.jpg", new List<string> { "1 cup Rice", "1 Onion", "2 Garlic Cloves", "1 Carrot", "1 tbsp Soy Sauce", "Salt", "Pepper" }, true, 0, 4, "Rice Pilaf", 0, new DateTime(2024, 12, 9, 8, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "10m", new DateTime(2024, 7, 28, 22, 11, 18, 0, DateTimeKind.Utc), "A quick and easy recipe for creamy scrambled eggs, perfect for breakfast.", "https://i.imgur.com/rxAzMjR.jpg", new List<string> { "2 Eggs", "2 tbsp Milk", "1 tbsp Butter", "Salt", "Pepper" }, true, 0, 2, "Classic Scrambled Eggs", 0, new DateTime(2024, 7, 28, 22, 11, 18, 0, DateTimeKind.Utc), 0 },
                    { new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "45m", new DateTime(2024, 12, 13, 12, 0, 0, 0, DateTimeKind.Utc), "A hearty spaghetti bolognese with minced beef and tomato sauce.", "https://cdn.stoneline.de/media/c5/63/4f/1727429313/spaghetti-bolognese.jpeg", new List<string> { "200g Spaghetti", "200g Minced Beef", "1 Onion", "2 Garlic Cloves", "4 Tomatoes", "Salt", "Pepper" }, true, 0, 4, "Spaghetti Bolognese", 0, new DateTime(2024, 12, 13, 12, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "40m", new DateTime(2023, 12, 24, 19, 1, 34, 0, DateTimeKind.Utc), "A comforting tomato soup made from fresh tomatoes and spices.", "https://i.imgur.com/SzhMVWs.jpg", new List<string> { "4 Ripe Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" }, true, 0, 2, "Tomato Soup", 0, new DateTime(2023, 12, 24, 19, 1, 34, 0, DateTimeKind.Utc), 0 },
                    { new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "30m", new DateTime(2024, 12, 2, 1, 30, 0, 0, DateTimeKind.Utc), "A creamy mushroom soup with fresh mushrooms and spices.", "https://www.mushroomcouncil.com/wp-content/uploads/2017/11/mushroom-soup-3-scaled.jpg", new List<string> { "200g Mushrooms", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" }, true, 0, 4, "Mushroom Soup", 0, new DateTime(2024, 12, 2, 1, 30, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "50m", new DateTime(2024, 12, 17, 16, 0, 0, 0, DateTimeKind.Utc), "Grilled chicken with a smoky BBQ sauce.", "https://www.allrecipes.com/thmb/APtZNY1GgOf3Ph0JUc-j4dImjrU=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/2467480-southern-bbq-chicken-Allrecipes-Magazine-4x3-1-3e180dccbaae446c8c2d05f708611fc6.jpg", new List<string> { "4 Chicken Breasts", "BBQ Sauce", "Salt", "Pepper" }, true, 0, 4, "BBQ Chicken", 0, new DateTime(2024, 12, 17, 16, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "10m", new DateTime(2024, 12, 5, 4, 0, 0, 0, DateTimeKind.Utc), "A classic breakfast combination of bacon and eggs.", "https://lowcarbinspirations.com/wp-content/uploads/2021/08/Classic-Bacon-and-Eggs-Recipe-3.jpg", new List<string> { "2 Eggs", "100g Bacon", "Salt", "Pepper" }, true, 0, 2, "Bacon and Eggs", 0, new DateTime(2024, 12, 5, 4, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "25m", new DateTime(2024, 12, 10, 9, 0, 0, 0, DateTimeKind.Utc), "Mushrooms stuffed with cheese and baked to perfection.", "https://www.inspiredtaste.net/wp-content/uploads/2018/09/Easy-Stuffed-Mushrooms-Recipe-1200.jpg", new List<string> { "200g Mushrooms", "100g Cheese", "2 Garlic Cloves", "1 tbsp Butter", "Salt", "Pepper" }, true, 0, 2, "Cheese Stuffed Mushrooms", 0, new DateTime(2024, 12, 10, 9, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "25m", new DateTime(2024, 12, 12, 11, 0, 0, 0, DateTimeKind.Utc), "Creamy mashed potatoes with roasted garlic.", "https://www.allrecipes.com/thmb/ytnCq3jVoAyGzGxm_oZxqGI-HCU=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/18290-garlic-mashed-potatoes-ddmfs-beauty2-4x3-0327-2-47384a10cded40ae90e574bc7fdb9433.jpg", new List<string> { "4 Potatoes", "2 Garlic Cloves", "50g Butter", "Salt", "Pepper" }, true, 0, 4, "Garlic Mashed Potatoes", 0, new DateTime(2024, 12, 12, 11, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "15m", new DateTime(2024, 12, 14, 13, 0, 0, 0, DateTimeKind.Utc), "A quick and healthy stir-fry with carrots and broccoli.", "https://mojo.generalmills.com/api/public/content/PcgVKEIG0UCHstblaGsBYg_gmi_hi_res_jpeg.jpeg?v=d91eed52&t=466b54bb264e48b199fc8e83ef1136b4", new List<string> { "1 Carrot", "1 Broccoli Head", "Soy Sauce", "Olive Oil", "Salt", "Pepper" }, true, 0, 2, "Carrot and Broccoli Stir-Fry", 0, new DateTime(2024, 12, 14, 13, 0, 0, 0, DateTimeKind.Utc), 0 }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "Category", "Code", "CreatedAt", "ImageUrl", "IsActive", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { new Guid("042dd338-3338-45a1-96c6-a1e2d0ca71ff"), "DISHTYPE", "SEAFOOD", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3319), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196329/default_storage/tag/dishtype/jhxdroetbjq9f57cixwj.png", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3320), "Seafood" },
                    { new Guid("2bf7f026-e745-4bd9-8701-a9519742d0f7"), "INGREDIENT", "EGG", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3264), "https://i.imgur.com/BAT5qyL.png", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3264), "Egg" },
                    { new Guid("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495"), "INGREDIENT", "CHEESE", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3279), "https://i.imgur.com/feglS7k.jpg", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3280), "Cheese" },
                    { new Guid("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8"), "INGREDIENT", "SOY_SAUCE", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3296), "https://i.imgur.com/2QiWJWH.jpg", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3296), "Soy Sauce" },
                    { new Guid("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6"), "INGREDIENT", "BROCCOLI", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3293), "https://i.imgur.com/8nDcffy.png", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3293), "Broccoli" },
                    { new Guid("6c91b894-d6dd-4c9b-a106-bfd029ce9e16"), "DISHTYPE", "NOODLES", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3302), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196327/default_storage/tag/dishtype/ingre9ficyzq0iitiwmd.png", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3303), "Noodles" },
                    { new Guid("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"), "INGREDIENT", "BUTTER", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3276), "https://i.imgur.com/Z8y4Hsr.jpg", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3277), "Butter" },
                    { new Guid("71676633-493e-46c5-86a0-21773f196035"), "DISHTYPE", "ALL", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3299), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196328/default_storage/tag/dishtype/hivalxshuvx5nnlfp5t1.png", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3300), "All" },
                    { new Guid("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e"), "INGREDIENT", "GARLIC", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3286), "https://i.imgur.com/oLwdHvx.jpg", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3287), "Garlic" },
                    { new Guid("92316e11-fd87-4c0f-aac7-bde4f19c2b38"), "INGREDIENT", "RICE", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3267), "https://i.imgur.com/C4nNmU1.png", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3268), "Rice" },
                    { new Guid("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6"), "INGREDIENT", "BACON", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3283), "https://i.imgur.com/lyYgVRi.jpg", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3284), "Bacon" },
                    { new Guid("a7a1953d-027e-43b6-ad0d-d10312a3064d"), "INGREDIENT", "MUSHROOM", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3270), "https://i.imgur.com/m8wBuYO.png", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3271), "Mushroom" },
                    { new Guid("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e"), "INGREDIENT", "CARROT", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3289), "https://i.imgur.com/DZEq7TK.jpg", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3290), "Carrot" },
                    { new Guid("d8b74fc2-f848-41af-a53f-20170aa453cd"), "DISHTYPE", "BBQ", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3308), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196328/default_storage/tag/dishtype/zl1f9g0dxxbtw2f4jiel.png", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3309), "BBQ" },
                    { new Guid("de3698bf-ad8b-4cc6-8ed0-662bd7eca486"), "DISHTYPE", "SPICE", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3305), "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196323/default_storage/tag/dishtype/frgtox8kb4edsvmutmez.png", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3306), "Spice" },
                    { new Guid("df3f6301-3cae-480a-87da-c7b8f6150292"), "INGREDIENT", "TOMATO", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3252), "https://i.imgur.com/3NovRt2.png", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3261), "Tomato" },
                    { new Guid("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3"), "INGREDIENT", "MILK", new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3273), "https://i.imgur.com/Rk3MwdQ.jpg", true, new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3274), "Milk" }
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
                columns: new[] { "Id", "AttachedImageUrls", "Content", "CreatedAt", "OdinalNumber", "RecipeId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("002373cd-c321-4326-bcee-f1376c88e238"), null, "Drain the cooked spaghetti and toss with the crispy bacon.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("036e4c18-e064-44ea-a4b1-ccbf7b236a07"), null, "Spread the mixture onto sliced baguette.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("04508e64-caab-46bc-9f98-1fc9d0cbabd1"), null, "Crack the eggs into a bowl and whisk with salt, pepper, and cheese.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0679e33c-43c8-4446-b86a-82a7a38724f2"), null, "Add chopped tomatoes and vegetable stock to the pot and simmer for 20 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0cbc0088-8ef1-48b9-83d7-55b6c98f9a87"), null, "Melt butter in a pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0d4fc64c-21c8-4521-a6a4-9f3d47db1dc4"), null, "Pour the egg mixture into the pan and cook until set, gently folding the edges.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0f2996e8-b4e9-4761-b9f9-f58b58fb4b83"), null, "Cook for 20 minutes, or until rice is tender and water is absorbed.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0f928b79-1146-4e3c-b1cf-542577f02ec7"), null, "Chop the onion, garlic, and tomatoes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("124e8a5b-d758-404a-8e3e-9aebcf0d389b"), null, "Season with salt, pepper, and fresh basil.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("13a22e3e-fda1-4cd4-8a0f-742d359006a2"), null, "Pour in the vegetable stock and let the soup simmer for 20 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1437c5b2-6bbd-4cf8-88b9-71bb01f6f5f9"), null, "Pour the egg mixture into the pan and gently stir until softly set.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("144884dd-35d7-4112-ace6-0e7141f01e90"), null, "Heat the olive oil in a pan over medium-high heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("160404ba-599f-40f8-afe4-dd28c583809f"), null, "Sauté onion and garlic in olive oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1fda2a0e-4478-4d11-8802-864e922b9b59"), null, "Blend the soup until smooth and serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("244f1c9e-5fb2-4173-a1ee-65a7bddf6070"), null, "Peel and devein the shrimp.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("24c65916-5483-4ce2-8aef-aa8bdc66ddfb"), null, "Cook the spaghetti according to package instructions and drain.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("26618d5f-ea76-477c-a342-9086ed61afe0"), null, "Scramble the eggs in a pan with some oil or butter.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("280329aa-2a02-4d4a-9681-6df8c6f6632e"), null, "Bake in the oven for 10-12 minutes until golden.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2dff8b9c-a697-4c87-8a1c-1451d8296e8d"), null, "Stuff the mushroom caps with the cheese mixture and place on a baking sheet.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2ee6794b-dae6-4f99-a09f-3f8593ee7cfd"), null, "Serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("330adf86-852a-436f-bce8-676595dd2b42"), null, "Add the broccoli and continue stir-frying for another 5 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("34cf5a22-c76d-4521-a3d9-55fa041bca3d"), null, "Add the chopped mushrooms and cook until they release their juices.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("34e86fae-f742-4e0d-a500-b5e91b15a19e"), null, "Boil the potatoes in salted water until tender, about 15 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("35d9f992-361a-43ef-b717-00ccd7f5e47b"), null, "Add the shrimp to the pan and cook until pink and opaque.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3d797d53-c2fa-49ab-9a4a-595ac606e686"), null, "In a bowl, whisk together the eggs and Parmesan cheese.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3e704a49-bed5-4d87-a5e7-a820858fb699"), null, "Sauté the onion in butter until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("40fa8bca-5273-48fe-84c1-151f08b9e20c"), null, "Season the eggs with salt and pepper, then serve with the crispy bacon.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44711740-1ca2-4e4d-ab62-07fa41f0d7c7"), null, "Bake in the oven for 20 minutes, or until the mushrooms are tender and the cheese is melted.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4ab77ae6-8575-43a3-b960-a63beba88cdc"), null, "Add the rice and soy sauce, then cook for 1-2 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4ae36fed-03a4-453f-ba63-bd55d1388b58"), null, "Mix eggs and grated parmesan in a bowl.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4be7f0bc-1c02-4a6c-aaec-c545c311d9d5"), null, "Season with salt and pepper to taste, and serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4e534748-482b-48ae-a1f3-223f4087ccad"), null, "Bake in the oven for 20-25 minutes, until cheese is melted and bubbly.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4e67eed5-6b75-4b4d-b78f-b03276459eef"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5099ea31-7c0c-430e-a7e3-3a877368dac1"), null, "Peel and chop the carrots, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("50fc3c23-1469-498b-b50d-e70892d1d290"), null, "Blend the soup until smooth, then serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("577f43f5-0096-4490-a8a9-ecb5c65b2ed9"), null, "Season the chicken breasts with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5baa9ad7-5a40-421d-9426-1cb6cc0e536d"), null, "Crack the eggs into the pan and cook to your desired doneness.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("61040e4a-f40d-430c-b201-5bca6f4b64f4"), null, "Add vegetables and stir-fry for 5-7 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("61711833-09a9-4bfd-bb97-5759ff04b50f"), null, "Blend until smooth.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("634b927e-1539-419a-b33b-c9397bfa0aee"), null, "Remove the stems from the mushrooms and chop them finely.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6410811f-6e12-4cab-b278-4529428d1fb6"), null, "Add soy sauce and stir well.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("65308f7a-bfa9-4161-8629-829b3160e772"), null, "Heat olive oil in a wok or large pan.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("65f0d43f-ac24-4e51-8486-4207fa2f1f28"), null, "Cook the minced beef in a pan until browned.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6c31d210-fce3-4ab0-897b-dfafc5c30271"), null, "Grill the chicken for 6-7 minutes on each side.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6dde54cc-4252-462b-b5f0-375a4743cae8"), null, "Melt butter in a non-stick pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("71c7eaef-b7e1-45c2-b41e-36dc14af08ef"), null, "Crack the eggs into a bowl and whisk with milk, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("754ce26b-dc93-42f3-8636-34ad860fe730"), null, "Serve with additional BBQ sauce on the side.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("75847d5f-b119-48bb-afed-157cc16d377b"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("797e0938-66be-45bd-baca-b02b1375959a"), null, "Cook spaghetti in salted boiling water until al dente.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7b244fb8-8c43-4a4f-9764-5e2bbb181177"), null, "Season with salt and pepper to taste.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7b726fcc-63c4-4c33-ad94-a3d768be89d3"), null, "Add the cooked rice to the pan and stir-fry with soy sauce.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7c768cf5-b351-4341-a11b-f4f47f0f3f1c"), null, "Toss cooked spaghetti with bacon and remove from heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7c86e6e3-c9f8-4fd1-b00a-ca4e85f02ac9"), null, "Cook rice according to package instructions.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7d4c3896-3f75-4082-bab6-ef9a02fbb673"), null, "Serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("80c622f6-c66a-490a-9eb5-32dadffbccd3"), null, "Brush the chicken with BBQ sauce during the last 2 minutes of grilling.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("814b37a2-ea2d-4f8f-92aa-2f26f939f356"), null, "Serve the Bolognese sauce over the cooked spaghetti.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("820e1fc7-c5ce-48ff-9fbb-8eb77caa95d5"), null, "Combine the browned beef with the tomato sauce, and simmer for 15 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("823b58eb-57df-4433-9d8e-45a910636e45"), null, "Season with soy sauce, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8792cf3f-973e-4fde-ae06-204461d2225d"), null, "Add tomatoes and vegetable stock, then simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8a31fa0f-a3eb-4f68-a7f0-f67195ff9cc8"), null, "Fry bacon until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9416ff68-2394-44a3-a7a4-708f0b146eee"), null, "Pour the egg and cheese mixture over the pasta and toss quickly to coat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("96786985-5ea6-4047-acc2-9b26e492ae9e"), null, "Peel and chop the potatoes into chunks.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("98fde936-8738-46ae-b145-bd67692126c3"), null, "Sauté the onions and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9fa4a5d6-0bc0-47fe-b408-1c05977dffe7"), null, "Mix the sautéed mushrooms with cheese and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9fd7f611-7c49-4ad8-a666-9a37293ca824"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9fd7f7b7-4998-4617-96f3-f48c69e4476c"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a0a168ad-ccd8-4ff6-8cb6-ab24b75d2c87"), null, "Melt butter in a pan and sauté garlic until fragrant.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a2523826-e1d7-4bde-abe2-ac33dfea5475"), null, "Add milk, ice cream, and sugar to a blender.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a3ed4267-5379-4dd0-be2b-8a44e8fa3946"), null, "Add the carrots and stir-fry for 3-4 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a88a99b4-cb4c-4890-96df-3825776b3be7"), null, "Mix softened butter with minced garlic and parsley.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("aa7cf4cb-274f-465e-ae49-246f9cfb78fe"), null, "Add chopped tomatoes to the pan with the onions and garlic, and cook for 10 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ab49ec4f-0b21-403f-b833-835331f23ac1"), null, "Add chopped carrots and garlic to the pan, and cook until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ad637a25-7ccf-424a-bc25-232962c48fbc"), null, "Preheat the grill to medium-high heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("aee49ef5-0b2b-4112-bfb2-052a9319a78b"), null, "Sauté the onion, garlic, and carrot in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b0a330f5-d837-45a2-9580-fab8e929292e"), null, "Season with salt and pepper, then blend the soup until smooth.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b0d08ac6-0a05-483f-96f1-3449d0c91aa6"), null, "Sauté the onion and garlic in a separate pan until softened.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b3522cd1-49ef-4536-b1a7-a562010a7235"), null, "Cook the spaghetti according to the package instructions.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b560505c-a826-4bf9-ac80-00c706a3ff1c"), null, "Add 2 cups of water and bring to a boil, then reduce the heat to low and cover.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b6ccbb8d-3443-4f11-b94d-75f986a91521"), null, "Chop all vegetables into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b8f4c726-c5b5-4eed-afe9-dc5bf8941587"), null, "Mash the potatoes with butter, roasted garlic, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("be18a840-49ef-44a1-89a3-8afde331a982"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c0dcf25c-23f9-4561-bb2d-c7112ac4ead4"), null, "Add chopped carrots and vegetable stock, and simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c396fdab-3053-4510-933e-7c7e7d687f32"), null, "Chop the mushrooms, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cacf7182-f273-4ef3-88ca-36872ddde97c"), null, "Season with salt, pepper, and parsley before serving.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("caef8804-734d-4271-a4d1-9b4d54bd282d"), null, "Combine steamed broccoli, sautéed onion, and cheese in a casserole dish.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cb1891dd-e82c-4044-99bc-3125b9a07285"), null, "Fry the bacon in a pan until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cb8d0f57-f677-4cdd-b834-63f6989ecf3b"), null, "Fry the bacon in a pan until crispy, then remove and chop.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cdbd4769-a7d4-4309-9a41-11a4b36df8a5"), null, "Chop the onion, garlic, and carrot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d11c1ee9-7860-40ce-b29a-6a564da8b58f"), null, "Chop the carrot and broccoli into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d4e50317-fad9-491f-beb4-0976985eaad6"), null, "Roast the garlic cloves in the oven for 10 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d5d5178c-435c-4e73-a0a4-617756c0a433"), null, "Steam the broccoli until tender.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d9bc34d7-8570-48c1-a54d-39dd0d9ddd39"), null, "Blend the soup until smooth and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e23221b0-9f0f-45b2-958c-0ed5b902695e"), null, "Sauté the onion and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e2baf9c3-3d8d-4d0d-900f-8a575a2e1968"), null, "Serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e2c1c808-a1c9-437d-856e-2e9dbc2a4e1c"), null, "Add the egg mixture and stir until creamy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ed3787c0-80a7-414d-bc20-9377037283b4"), null, "Sauté the chopped mushroom stems and garlic in butter until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f56e0f81-b057-4fa2-8eca-6ec89e9d64cb"), null, "Sauté the onion and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
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
                name: "Recipe");
        }
    }
}
