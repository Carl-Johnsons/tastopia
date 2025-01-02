using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIsAdminBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("f9a8c16e-610a-49f5-aac0-82183d8c3a16"),
                column: "IsAdmin",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "AccountId",
                keyValue: new Guid("f9a8c16e-610a-49f5-aac0-82183d8c3a16"),
                column: "IsAdmin",
                value: false);
        }
    }
}
