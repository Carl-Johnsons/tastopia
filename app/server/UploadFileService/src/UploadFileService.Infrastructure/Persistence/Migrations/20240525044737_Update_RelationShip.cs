using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UploadFileService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_RelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudinaryFile_ExtensionType_ExtensionTypeId",
                table: "CloudinaryFile");

            migrationBuilder.AddForeignKey(
                name: "FK_CloudinaryFile_ExtensionType_ExtensionTypeId",
                table: "CloudinaryFile",
                column: "ExtensionTypeId",
                principalTable: "ExtensionType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudinaryFile_ExtensionType_ExtensionTypeId",
                table: "CloudinaryFile");

            migrationBuilder.AddForeignKey(
                name: "FK_CloudinaryFile_ExtensionType_ExtensionTypeId",
                table: "CloudinaryFile",
                column: "ExtensionTypeId",
                principalTable: "ExtensionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
