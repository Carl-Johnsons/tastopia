using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DataType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    AvatarUrl = table.Column<string>(type: "text", nullable: false),
                    BackgroundUrl = table.Column<string>(type: "text", nullable: false),
                    Dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    TotalFollwer = table.Column<int>(type: "integer", nullable: true),
                    TotalFollowing = table.Column<int>(type: "integer", nullable: true),
                    TotalRecipe = table.Column<int>(type: "integer", nullable: true),
                    IsAccountActive = table.Column<bool>(type: "boolean", nullable: false),
                    AccountUsername = table.Column<string>(type: "text", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "UserFollow",
                columns: table => new
                {
                    FollowerId = table.Column<Guid>(type: "uuid", nullable: false),
                    FollowingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollow", x => new { x.FollowerId, x.FollowingId });
                    table.ForeignKey(
                        name: "FK_UserFollow_User_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "User",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFollow_User_FollowingId",
                        column: x => x.FollowingId,
                        principalTable: "User",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReport_User_AccountId",
                        column: x => x.AccountId,
                        principalTable: "User",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserReport_User_ReportedId",
                        column: x => x.ReportedId,
                        principalTable: "User",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSetting",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    SettingId = table.Column<Guid>(type: "uuid", nullable: false),
                    SettingValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSetting", x => new { x.AccountId, x.SettingId });
                    table.ForeignKey(
                        name: "FK_UserSetting_Setting_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Setting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSetting_User_AccountId",
                        column: x => x.AccountId,
                        principalTable: "User",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "Code", "DataType", "Description" },
                values: new object[,]
                {
                    { new Guid("5fad2a82-82db-430b-b61b-13704a91944a"), "LANGUAGE", "String", "Language for display data for website dashboard and mobile app" },
                    { new Guid("dad92eec-123c-4ae2-9848-b7f7a1a6ed56"), "THEME", "String", "Theme for website dashboard and mobile app" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "AccountId", "AccountUsername", "Address", "AvatarUrl", "BackgroundUrl", "Bio", "DisplayName", "Dob", "Gender", "IsAccountActive", "IsAdmin", "TotalFollowing", "TotalFollwer", "TotalRecipe" },
                values: new object[,]
                {
                    { new Guid("03e4b46e-b84a-43a9-a421-1b19e02023bb"), "raina1234", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Raina", null, null, true, false, null, null, null },
                    { new Guid("078ecc42-7643-4cff-b851-eeac5ba1bb29"), "bob1234", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Bob", null, null, true, false, null, null, null },
                    { new Guid("0ee35f28-21bf-49f9-ad89-b6a450c41908"), "aiden", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Aiden", null, null, true, false, 0, null, 0 },
                    { new Guid("0f368db8-84f3-499d-be8b-2daf685c6f5e"), "amelia", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Amelia", null, null, true, false, 0, null, 0 },
                    { new Guid("1cfb7c40-cccc-4a87-88a9-ff967d8dcddb"), "duc1234", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Duc", null, null, true, false, null, null, null },
                    { new Guid("2155d0ed-b998-416c-adaf-19f68a0a5b34"), "scarlett", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Scarlett", null, null, true, false, 0, null, 0 },
                    { new Guid("28fb3b5f-d2a3-4456-a6b5-dbf75cea4e0a"), "jackson", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Jackson", null, null, true, false, 0, null, 0 },
                    { new Guid("50e00c7f-39da-48d1-b273-3562225a5972"), "an1234", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "An", null, null, true, false, null, 1, null },
                    { new Guid("594a3fc8-3d24-4305-a9d7-569586d0604e"), "kara1234", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Kara", null, null, true, false, null, null, null },
                    { new Guid("5d02ff8b-62a6-425a-9828-6033112b54e0"), "lily", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Lily", null, null, true, false, 0, null, 0 },
                    { new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), "alice1234", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Alice", null, null, true, false, 4, null, 21 },
                    { new Guid("69a36c05-c7ff-4411-a283-fa801cbba5ee"), "mia", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Mia", null, null, true, false, 0, null, 0 },
                    { new Guid("6e411b44-26d3-490e-b4e5-8012e2cfd897"), "emma", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Emma", null, null, true, false, 0, null, 0 },
                    { new Guid("6e898d72-52d0-4de8-a784-5bb1f1a4eda5"), "logan", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Logan", null, null, true, false, 0, null, 0 },
                    { new Guid("6f37441a-92d8-4d27-aa1a-e50ab1a2b4b7"), "sophia", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Sophia", null, null, true, false, 0, null, 0 },
                    { new Guid("7201a43a-6a1d-4634-bc27-9cd71f90a11a"), "isabella", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Isabella", null, null, true, false, 0, null, 0 },
                    { new Guid("76346f0e-a52c-4d94-a909-4a8cc59c8ede"), "lainey1234", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Lainey", null, null, true, false, null, 1, null },
                    { new Guid("866a7cd5-da2a-46e4-abe3-8efe6bd6a1d0"), "ella", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Ella", null, null, true, false, 0, null, 0 },
                    { new Guid("8edf9219-7ba6-4259-a7e5-cd95b2e29ca2"), "james", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "James", null, null, true, false, 0, null, 0 },
                    { new Guid("936c85f2-6958-40fd-a201-74485ac917e0"), "alex", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Alex", null, null, true, false, 0, null, 0 },
                    { new Guid("9baca8d6-38d7-451e-bf2c-48652ddd7fca"), "lucas", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Lucas", null, null, true, false, 0, null, 0 },
                    { new Guid("9f23334a-1148-4b6e-b636-40cf448735dd"), "ava", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Ava", null, null, true, false, 0, null, 0 },
                    { new Guid("b1ccb0c7-34eb-4545-859d-d7307aa42ff7"), "carter", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Carter", null, null, true, false, 0, null, 0 },
                    { new Guid("bb06e4ec-f371-45d5-804e-22c65c77f67d"), "kian1234", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Kian", null, null, true, false, null, 1, null },
                    { new Guid("bb18d21b-985b-4a54-bc04-f6cf6ac0a32e"), "noah", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Noah", null, null, true, false, 0, null, 0 },
                    { new Guid("cd1c7fe9-3308-4afb-83f4-23fa1e9efba8"), "mac1234", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Mac", null, null, true, false, null, null, null },
                    { new Guid("dff9a8f3-c6c4-4d97-98f9-bd9a9a18b0cf"), "chloe", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Chloe", null, null, true, false, 0, null, 0 },
                    { new Guid("e6333cb5-7008-4fa2-b835-364e304180a3"), "grayson", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Grayson", null, null, true, false, 0, null, 0 },
                    { new Guid("e67ac48b-9dd0-42a4-9fa3-a243b00ca5dd"), "ethan", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Ethan", null, null, true, false, 0, null, 0 },
                    { new Guid("e797952f-1b76-4db9-81a4-8e2f5f9152ea"), "willa1234", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Willa", null, null, true, false, null, 1, null },
                    { new Guid("f9a8c16e-610a-49f5-aac0-82183d8c3a16"), "admin1234", null, "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png", "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png", null, "Admin", null, null, true, true, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "UserFollow",
                columns: new[] { "FollowerId", "FollowingId" },
                values: new object[,]
                {
                    { new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), new Guid("50e00c7f-39da-48d1-b273-3562225a5972") },
                    { new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), new Guid("76346f0e-a52c-4d94-a909-4a8cc59c8ede") },
                    { new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), new Guid("bb06e4ec-f371-45d5-804e-22c65c77f67d") },
                    { new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), new Guid("e797952f-1b76-4db9-81a4-8e2f5f9152ea") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFollow_FollowingId",
                table: "UserFollow",
                column: "FollowingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReport_AccountId",
                table: "UserReport",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReport_ReportedId",
                table: "UserReport",
                column: "ReportedId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSetting_SettingId",
                table: "UserSetting",
                column: "SettingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFollow");

            migrationBuilder.DropTable(
                name: "UserReport");

            migrationBuilder.DropTable(
                name: "UserSetting");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
