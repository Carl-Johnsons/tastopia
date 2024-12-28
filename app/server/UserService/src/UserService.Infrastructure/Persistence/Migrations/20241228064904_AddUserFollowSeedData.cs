using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFollowSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserFollow",
                keyColumns: new[] { "FollowerId", "FollowingId" },
                keyValues: new object[] { new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), new Guid("50e00c7f-39da-48d1-b273-3562225a5972") });

            migrationBuilder.DeleteData(
                table: "UserFollow",
                keyColumns: new[] { "FollowerId", "FollowingId" },
                keyValues: new object[] { new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), new Guid("76346f0e-a52c-4d94-a909-4a8cc59c8ede") });

            migrationBuilder.DeleteData(
                table: "UserFollow",
                keyColumns: new[] { "FollowerId", "FollowingId" },
                keyValues: new object[] { new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), new Guid("bb06e4ec-f371-45d5-804e-22c65c77f67d") });

            migrationBuilder.DeleteData(
                table: "UserFollow",
                keyColumns: new[] { "FollowerId", "FollowingId" },
                keyValues: new object[] { new Guid("61c61ac7-291e-4075-9689-666ef05547ed"), new Guid("e797952f-1b76-4db9-81a4-8e2f5f9152ea") });
        }
    }
}
