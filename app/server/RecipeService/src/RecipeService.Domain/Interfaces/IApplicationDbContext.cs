using Contract.Interfaces;
using RecipeService.Domain.Entities;

namespace RecipeService.Domain.Interfaces;


public interface IApplicationDbContext : IMongoDbContext
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    //Relationship
    public DbSet<RecipeTag> RecipeTags { get; set; }
    public DbSet<UserBookmarkRecipe> UserBookmarkRecipes { get; set; }
    public DbSet<UserReportRecipe> UserReportRecipes { get; set; }
    public DbSet<UserReportComment> UserReportComments { get; set; }

}
