using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmationExpiry",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmationOTP",
                table: "Account",
                newName: "PhoneOTP");

            migrationBuilder.AlterColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Account",
                type: "boolean",
                nullable: false,
                defaultValueSql: "False",
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "EmailConfirmed",
                table: "Account",
                type: "boolean",
                nullable: false,
                defaultValueSql: "False",
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<string>(
                name: "EmailOTP",
                table: "Account",
                type: "character varying(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailOTPCreated",
                table: "Account",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailOTPExpiry",
                table: "Account",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PhoneOTPCreated",
                table: "Account",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PhoneOTPExpiry",
                table: "Account",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestOTPCount",
                table: "Account",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailOTP",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "EmailOTPCreated",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "EmailOTPExpiry",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "PhoneOTPCreated",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "PhoneOTPExpiry",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "RequestOTPCount",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "PhoneOTP",
                table: "Account",
                newName: "EmailConfirmationOTP");

            migrationBuilder.AlterColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Account",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValueSql: "False");

            migrationBuilder.AlterColumn<bool>(
                name: "EmailConfirmed",
                table: "Account",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValueSql: "False");

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailConfirmationExpiry",
                table: "Account",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
