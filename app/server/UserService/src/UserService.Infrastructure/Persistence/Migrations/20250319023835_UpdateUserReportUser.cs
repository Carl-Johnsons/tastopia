using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserReportUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReport_User_AccountId",
                table: "UserReport");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "UserReport",
                newName: "ReporterId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReport_AccountId",
                table: "UserReport",
                newName: "IX_UserReport_ReporterId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReport_User_ReporterId",
                table: "UserReport",
                column: "ReporterId",
                principalTable: "User",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReport_User_ReporterId",
                table: "UserReport");

            migrationBuilder.RenameColumn(
                name: "ReporterId",
                table: "UserReport",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReport_ReporterId",
                table: "UserReport",
                newName: "IX_UserReport_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReport_User_AccountId",
                table: "UserReport",
                column: "AccountId",
                principalTable: "User",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
