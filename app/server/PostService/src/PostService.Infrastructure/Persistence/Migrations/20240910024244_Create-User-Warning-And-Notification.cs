using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserWarningAndNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentReplies",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReplyCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReplies", x => new { x.CommentId, x.ReplyCommentId });
                    table.ForeignKey(
                        name: "Comment_CommentReplies",
                        column: x => x.CommentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ReplyComment_CommentReplies",
                        column: x => x.ReplyCommentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserContentRestrictionsType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContentRestrictionsType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserWarning",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarningCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWarning", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserContentRestrictions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserContentRestrictionsTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContentRestrictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserContentRestrictions_UserContentRestrictionsType_UserContentRestrictionsTypeId",
                        column: x => x.UserContentRestrictionsTypeId,
                        principalTable: "UserContentRestrictionsType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplies_ReplyCommentId",
                table: "CommentReplies",
                column: "ReplyCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserContentRestrictions_UserContentRestrictionsTypeId",
                table: "UserContentRestrictions",
                column: "UserContentRestrictionsTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentReplies");

            migrationBuilder.DropTable(
                name: "UserContentRestrictions");

            migrationBuilder.DropTable(
                name: "UserWarning");

            migrationBuilder.DropTable(
                name: "UserContentRestrictionsType");
        }
    }
}
