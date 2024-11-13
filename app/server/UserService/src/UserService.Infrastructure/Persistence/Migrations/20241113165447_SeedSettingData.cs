using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedSettingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Code", "DataType", "Description" },
                values: new object[,]
                {
                    { new Guid("5fad2a82-82db-430b-b61b-13704a91944a"), "LANGUAGE", "String", "Language for display data for website dashboard and mobile app" },
                    { new Guid("dad92eec-123c-4ae2-9848-b7f7a1a6ed56"), "THEME", "String", "Theme for website dashboard and mobile app" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("5fad2a82-82db-430b-b61b-13704a91944a"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("dad92eec-123c-4ae2-9848-b7f7a1a6ed56"));
        }
    }
}
