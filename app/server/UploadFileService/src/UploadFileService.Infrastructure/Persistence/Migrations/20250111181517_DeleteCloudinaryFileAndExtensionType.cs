using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UploadFileService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCloudinaryFileAndExtensionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CloudinaryFile");

            migrationBuilder.DropTable(
                name: "ExtensionType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtensionType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtensionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CloudinaryFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtensionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PublicId = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudinaryFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CloudinaryFile_ExtensionType_ExtensionTypeId",
                        column: x => x.ExtensionTypeId,
                        principalTable: "ExtensionType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CloudinaryFile_ExtensionTypeId",
                table: "CloudinaryFile",
                column: "ExtensionTypeId");
        }
    }
}
