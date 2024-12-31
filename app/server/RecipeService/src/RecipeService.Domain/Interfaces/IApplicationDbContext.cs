using RecipeService.Domain.Entities;

namespace RecipeService.Domain.Interfaces;


public interface IApplicationDbContext : IDbContext
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<Comment> Comments { get; set; }
    //Relationship
    public DbSet<RecipeTag> RecipeTags { get; set; }
    public DbSet<RecipeVote> RecipeVotes { get; set; }
    public DbSet<CommentVote> CommentVotes { get; set; }

    public DbSet<UserBookmarkRecipe> UserBookmarkRecipes { get; set; }
    public DbSet<UserReportRecipe> UserReportRecipes { get; set; }
    public DbSet<UserReportComment> UserReportComments { get; set; }

}
