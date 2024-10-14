using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UploadFileService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Create_ExtensionType_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtensionType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtensionType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CloudinaryFile_ExtensionTypeId",
                table: "CloudinaryFile",
                column: "ExtensionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CloudinaryFile_ExtensionType_ExtensionTypeId",
                table: "CloudinaryFile",
                column: "ExtensionTypeId",
                principalTable: "ExtensionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudinaryFile_ExtensionType_ExtensionTypeId",
                table: "CloudinaryFile");

            migrationBuilder.DropTable(
                name: "ExtensionType");

            migrationBuilder.DropIndex(
                name: "IX_CloudinaryFile_ExtensionTypeId",
                table: "CloudinaryFile");
        }
    }
}
