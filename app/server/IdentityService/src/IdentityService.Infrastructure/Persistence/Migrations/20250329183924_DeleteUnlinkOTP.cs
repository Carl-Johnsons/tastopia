using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUnlinkOTP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnlinkEmailOTP",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "UnlinkPhoneOTP",
                table: "Account");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnlinkEmailOTP",
                table: "Account",
                type: "character varying(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnlinkPhoneOTP",
                table: "Account",
                type: "character varying(6)",
                maxLength: 6,
                nullable: true);
        }
    }
}
