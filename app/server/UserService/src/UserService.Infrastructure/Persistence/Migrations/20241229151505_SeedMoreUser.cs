using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "AccountId", "AccountUsername", "Address", "AvatarUrl", "BackgroundUrl", "Bio", "DisplayName", "Dob", "Gender", "IsAccountActive", "TotalFollowing", "TotalFollwer", "TotalRecipe" },
                values: new object[,]
                {
                    { new Guid("0ee35f28-21bf-49f9-ad89-b6a450c41908"), "aiden", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Aiden", null, null, true, 0, null, 0 },
                    { new Guid("0f368db8-84f3-499d-be8b-2daf685c6f5e"), "amelia", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Amelia", null, null, true, 0, null, 0 },
                    { new Guid("2155d0ed-b998-416c-adaf-19f68a0a5b34"), "scarlett", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Scarlett", null, null, true, 0, null, 0 },
                    { new Guid("28fb3b5f-d2a3-4456-a6b5-dbf75cea4e0a"), "jackson", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Jackson", null, null, true, 0, null, 0 },
                    { new Guid("5d02ff8b-62a6-425a-9828-6033112b54e0"), "lily", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Lily", null, null, true, 0, null, 0 },
                    { new Guid("69a36c05-c7ff-4411-a283-fa801cbba5ee"), "mia", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Mia", null, null, true, 0, null, 0 },
                    { new Guid("6e411b44-26d3-490e-b4e5-8012e2cfd897"), "emma", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Emma", null, null, true, 0, null, 0 },
                    { new Guid("6e898d72-52d0-4de8-a784-5bb1f1a4eda5"), "logan", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Logan", null, null, true, 0, null, 0 },
                    { new Guid("6f37441a-92d8-4d27-aa1a-e50ab1a2b4b7"), "sophia", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Sophia", null, null, true, 0, null, 0 },
                    { new Guid("7201a43a-6a1d-4634-bc27-9cd71f90a11a"), "isabella", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Isabella", null, null, true, 0, null, 0 },
                    { new Guid("866a7cd5-da2a-46e4-abe3-8efe6bd6a1d0"), "ella", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Ella", null, null, true, 0, null, 0 },
                    { new Guid("8edf9219-7ba6-4259-a7e5-cd95b2e29ca2"), "james", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "James", null, null, true, 0, null, 0 },
                    { new Guid("936c85f2-6958-40fd-a201-74485ac917e0"), "alex", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Alex", null, null, true, 0, null, 0 },
                    { new Guid("9baca8d6-38d7-451e-bf2c-48652ddd7fca"), "lucas", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Lucas", null, null, true, 0, null, 0 },
                    { new Guid("9f23334a-1148-4b6e-b636-40cf448735dd"), "ava", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Ava", null, null, true, 0, null, 0 },
                    { new Guid("b1ccb0c7-34eb-4545-859d-d7307aa42ff7"), "carter", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Carter", null, null, true, 0, null, 0 },
                    { new Guid("bb18d21b-985b-4a54-bc04-f6cf6ac0a32e"), "noah", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Noah", null, null, true, 0, null, 0 },
                    { new Guid("dff9a8f3-c6c4-4d97-98f9-bd9a9a18b0cf"), "chloe", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Chloe", null, null, true, 0, null, 0 },
                    { new Guid("e6333cb5-7008-4fa2-b835-364e304180a3"), "grayson", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Grayson", null, null, true, 0, null, 0 },
                    { new Guid("e67ac48b-9dd0-42a4-9fa3-a243b00ca5dd"), "ethan", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Ethan", null, null, true, 0, null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("0ee35f28-21bf-49f9-ad89-b6a450c41908"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("0f368db8-84f3-499d-be8b-2daf685c6f5e"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("2155d0ed-b998-416c-adaf-19f68a0a5b34"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("28fb3b5f-d2a3-4456-a6b5-dbf75cea4e0a"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("5d02ff8b-62a6-425a-9828-6033112b54e0"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("69a36c05-c7ff-4411-a283-fa801cbba5ee"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("6e411b44-26d3-490e-b4e5-8012e2cfd897"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("6e898d72-52d0-4de8-a784-5bb1f1a4eda5"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("6f37441a-92d8-4d27-aa1a-e50ab1a2b4b7"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("7201a43a-6a1d-4634-bc27-9cd71f90a11a"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("866a7cd5-da2a-46e4-abe3-8efe6bd6a1d0"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("8edf9219-7ba6-4259-a7e5-cd95b2e29ca2"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("936c85f2-6958-40fd-a201-74485ac917e0"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("9baca8d6-38d7-451e-bf2c-48652ddd7fca"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("9f23334a-1148-4b6e-b636-40cf448735dd"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("b1ccb0c7-34eb-4545-859d-d7307aa42ff7"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("bb18d21b-985b-4a54-bc04-f6cf6ac0a32e"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("dff9a8f3-c6c4-4d97-98f9-bd9a9a18b0cf"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("e6333cb5-7008-4fa2-b835-364e304180a3"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("e67ac48b-9dd0-42a4-9fa3-a243b00ca5dd"));
        }
    }
}
