using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCloneAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReport_User_UserId",
                table: "UserReport");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSetting_User_UserId",
                table: "UserSetting");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserSetting",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserReport",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReport_UserId",
                table: "UserReport",
                newName: "IX_UserReport_AccountId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "AccountId");

            migrationBuilder.AddColumn<string>(
                name: "AccountUsername",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccountActive",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("03e4b46e-b84a-43a9-a421-1b19e02023bb"),
                columns: new[] { "AccountUsername", "IsAccountActive" },
                values: new object[] { "raina1234", true });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("078ecc42-7643-4cff-b851-eeac5ba1bb29"),
                columns: new[] { "AccountUsername", "IsAccountActive" },
                values: new object[] { "bob1234", true });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("1cfb7c40-cccc-4a87-88a9-ff967d8dcddb"),
                columns: new[] { "AccountUsername", "IsAccountActive" },
                values: new object[] { "duc1234", true });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("50e00c7f-39da-48d1-b273-3562225a5972"),
                columns: new[] { "AccountUsername", "IsAccountActive", "TotalFollwer" },
                values: new object[] { "an1234", true, 1 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                columns: new[] { "AccountUsername", "IsAccountActive" },
                values: new object[] { "kara1234", true });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("61c61ac7-291e-4075-9689-666ef05547ed"),
                columns: new[] { "AccountUsername", "IsAccountActive", "TotalFollowing", "TotalRecipe" },
                values: new object[] { "alice1234", true, 4, 21 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("76346f0e-a52c-4d94-a909-4a8cc59c8ede"),
                columns: new[] { "AccountUsername", "IsAccountActive", "TotalFollwer" },
                values: new object[] { "lainey1234", true, 1 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                columns: new[] { "AccountUsername", "IsAccountActive", "TotalFollwer" },
                values: new object[] { "kian1234", true, 1 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("cd1c7fe9-3308-4afb-83f4-23fa1e9efba8"),
                columns: new[] { "AccountUsername", "IsAccountActive" },
                values: new object[] { "mac1234", true });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("e797952f-1b76-4db9-81a4-8e2f5f9152ea"),
                columns: new[] { "AccountUsername", "IsAccountActive", "TotalFollwer" },
                values: new object[] { "willa1234", true, 1 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("f9a8c16e-610a-49f5-aac0-82183d8c3a16"),
                columns: new[] { "AccountUsername", "IsAccountActive" },
                values: new object[] { "admin1234", true });

            migrationBuilder.AddForeignKey(
                name: "FK_UserReport_User_AccountId",
                table: "UserReport",
                column: "AccountId",
                principalTable: "User",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSetting_User_AccountId",
                table: "UserSetting",
                column: "AccountId",
                principalTable: "User",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReport_User_AccountId",
                table: "UserReport");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSetting_User_AccountId",
                table: "UserSetting");

            migrationBuilder.DropColumn(
                name: "AccountUsername",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsAccountActive",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "UserSetting",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "UserReport",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReport_AccountId",
                table: "UserReport",
                newName: "IX_UserReport_UserId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "User",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("50e00c7f-39da-48d1-b273-3562225a5972"),
                column: "TotalFollwer",
                value: null);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("61c61ac7-291e-4075-9689-666ef05547ed"),
                columns: new[] { "TotalFollowing", "TotalRecipe" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("76346f0e-a52c-4d94-a909-4a8cc59c8ede"),
                column: "TotalFollwer",
                value: null);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                column: "TotalFollwer",
                value: null);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("e797952f-1b76-4db9-81a4-8e2f5f9152ea"),
                column: "TotalFollwer",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReport_User_UserId",
                table: "UserReport",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSetting_User_UserId",
                table: "UserSetting",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
