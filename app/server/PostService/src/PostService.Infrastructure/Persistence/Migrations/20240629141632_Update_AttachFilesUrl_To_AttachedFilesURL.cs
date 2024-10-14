using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_AttachFilesUrl_To_AttachedFilesURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachFilesUrl",
                table: "Post",
                newName: "AttachedFilesURL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachedFilesURL",
                table: "Post",
                newName: "AttachFilesUrl");
        }
    }
}
