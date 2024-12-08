using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIngredientEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserReportRecipe_RecipeId",
                table: "UserReportRecipe",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReportRecipe_Recipes_RecipeId",
                table: "UserReportRecipe",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReportRecipe_Recipes_RecipeId",
                table: "UserReportRecipe");

            migrationBuilder.DropIndex(
                name: "IX_UserReportRecipe_RecipeId",
                table: "UserReportRecipe");
        }
    }
}
