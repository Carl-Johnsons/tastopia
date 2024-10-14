using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatepostInteractentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostInteract",
                table: "PostInteract");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostInteract",
                table: "PostInteract",
                columns: new[] { "PostId", "InteractionId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostInteract",
                table: "PostInteract");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostInteract",
                table: "PostInteract",
                columns: new[] { "PostId", "InteractionId" });
        }
    }
}
