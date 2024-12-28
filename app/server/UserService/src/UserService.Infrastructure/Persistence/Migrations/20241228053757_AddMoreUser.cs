using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("d47cb12a-2a20-4151-97c9-d0b85b668799"));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "AvatarUrl", "BackgroundUrl", "Bio", "DisplayName", "Dob", "Gender", "TotalFollowing", "TotalFollwer", "TotalRecipe" },
                values: new object[,]
                {
                    { new Guid("03e4b46e-b84a-43a9-a421-1b19e02023bb"), null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Raina", null, null, null, null, null },
                    { new Guid("078ecc42-7643-4cff-b851-eeac5ba1bb29"), null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Bob", null, null, null, null, null },
                    { new Guid("1cfb7c40-cccc-4a87-88a9-ff967d8dcddb"), null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Duc", null, null, null, null, null },
                    { new Guid("50e00c7f-39da-48d1-b273-3562225a5972"), null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "An", null, null, null, null, null },
                    { new Guid("594a3fc8-3d24-4305-a9d7-569586d0604e"), null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Kara", null, null, null, null, null },
                    { new Guid("76346f0e-a52c-4d94-a909-4a8cc59c8ede"), null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Lainey", null, null, null, null, null },
                    { new Guid("bb06e4ec-f371-45d5-804e-22c65c77f67d"), null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Kian", null, null, null, null, null },
                    { new Guid("cd1c7fe9-3308-4afb-83f4-23fa1e9efba8"), null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Mac", null, null, null, null, null },
                    { new Guid("e797952f-1b76-4db9-81a4-8e2f5f9152ea"), null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Willa", null, null, null, null, null },
                    { new Guid("f9a8c16e-610a-49f5-aac0-82183d8c3a16"), null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Admin", null, null, null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("03e4b46e-b84a-43a9-a421-1b19e02023bb"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("078ecc42-7643-4cff-b851-eeac5ba1bb29"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1cfb7c40-cccc-4a87-88a9-ff967d8dcddb"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("50e00c7f-39da-48d1-b273-3562225a5972"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("594a3fc8-3d24-4305-a9d7-569586d0604e"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("76346f0e-a52c-4d94-a909-4a8cc59c8ede"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bb06e4ec-f371-45d5-804e-22c65c77f67d"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cd1c7fe9-3308-4afb-83f4-23fa1e9efba8"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("e797952f-1b76-4db9-81a4-8e2f5f9152ea"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("f9a8c16e-610a-49f5-aac0-82183d8c3a16"));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "AvatarUrl", "BackgroundUrl", "Bio", "DisplayName", "Dob", "Gender", "TotalFollowing", "TotalFollwer", "TotalRecipe" },
                values: new object[] { new Guid("d47cb12a-2a20-4151-97c9-d0b85b668799"), null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Kian", null, null, null, null, null });
        }
    }
}
