using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeService.Domain.Entities;
using RecipeService.Infrastructure.Utilities;

namespace RecipeService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
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

    public DbContext Instance => this;
    public ApplicationDbContext()
    {
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        EnvUtility.LoadEnvFile();

        var db = DotNetEnv.Env.GetString("DB", "RecipeDB").Trim();
        var mongoConnectionString = EnvUtility.GetMongoDBConnectionString();

        optionsBuilder.UseMongoDB(mongoConnectionString, db);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseAuditableEntity).IsAssignableFrom(entityType.ClrType))
            {
                var createdAtProperty = entityType.FindProperty("CreatedAt");
                var updatedAtProperty = entityType.FindProperty("UpdatedAt");

                if (createdAtProperty != null)
                {
                    createdAtProperty.SetValueConverter(
                        new ValueConverter<DateTime, DateTime>(
                            v => v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }

                if (updatedAtProperty != null)
                {
                    updatedAtProperty.SetValueConverter(
                        new ValueConverter<DateTime, DateTime>(
                            v => v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
            }
        }
        //time
        modelBuilder.Entity<Recipe>();
        modelBuilder.Entity<Step>();
        modelBuilder.Entity<Tag>();
        modelBuilder.Entity<Comment>();
        modelBuilder.Entity<UserReportComment>();
        modelBuilder.Entity<UserReportRecipe>();

        //one to many
        modelBuilder.Entity<Step>()
            .HasOne(s => s.Recipe)
            .WithMany(r => r.Steps)
            .HasForeignKey(s => s.RecipeId);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Recipe)
            .WithMany(r => r.Comments)
            .HasForeignKey(s => s.RecipeId);

        modelBuilder.Entity<RecipeVote>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeVotes)
            .HasForeignKey(ri => ri.RecipeId);

        modelBuilder.Entity<CommentVote>()
            .HasOne(cv => cv.Comment)
            .WithMany(c => c.CommentVotes)
            .HasForeignKey(ci => ci.CommentId);

        //many to many
        modelBuilder.Entity<RecipeTag>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeTags)
            .HasForeignKey(ri => ri.RecipeId);

        //modelBuilder.Entity<Tag>().HasData(TagData.Data);
        //modelBuilder.Entity<Recipe>().HasData(RecipeData.Recipe);
        //modelBuilder.Entity<Step>().HasData(RecipeData.Step);
        //modelBuilder.Entity<RecipeTag>().HasData(RecipeTagData.Data);
        //modelBuilder.Entity<RecipeVote>().HasData(RecipeVoteData.Data);
        //modelBuilder.Entity<Comment>().HasData(CommentData.Data);

    }
}
