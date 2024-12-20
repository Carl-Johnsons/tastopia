using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeService.Domain.Entities;
using RecipeService.Infrastructure.Persistence.Mockup.Data;
using RecipeService.Infrastructure.Utilities;

namespace RecipeService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext()
    {
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbContext Instance => this;
   
    public DbSet<Recipe> Recipes {  get; set; }
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


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(EnvUtility.GetConnectionString(), option =>
        {
            option.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(15),
                    errorCodesToAdd: null
                );
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(Domain.Common.BaseAuditableEntity).IsAssignableFrom(entityType.ClrType))
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
        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                  .HasConversion(v => v.ToUniversalTime(),
                                 v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            entity.Property(e => e.UpdatedAt)
                  .HasConversion(v => v.ToUniversalTime(),
                                 v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        });
        modelBuilder.Entity<Step>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                  .HasConversion(v => v.ToUniversalTime(),
                                 v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            entity.Property(e => e.UpdatedAt)
                  .HasConversion(v => v.ToUniversalTime(),
                                 v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        });
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                  .HasConversion(v => v.ToUniversalTime(),
                                 v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            entity.Property(e => e.UpdatedAt)
                  .HasConversion(v => v.ToUniversalTime(),
                                 v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        });
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                  .HasConversion(v => v.ToUniversalTime(),
                                 v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            entity.Property(e => e.UpdatedAt)
                  .HasConversion(v => v.ToUniversalTime(),
                                 v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        });

        modelBuilder.Entity<UserReportComment>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                  .HasConversion(v => v.ToUniversalTime(),
                                 v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            entity.Property(e => e.UpdatedAt)
                  .HasConversion(v => v.ToUniversalTime(),
                                 v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        });

        modelBuilder.Entity<UserReportRecipe>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                  .HasConversion(v => v.ToUniversalTime(),
                                 v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            entity.Property(e => e.UpdatedAt)
                  .HasConversion(v => v.ToUniversalTime(),
                                 v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        });


        //one to many
        modelBuilder.Entity<Step>()
            .HasOne(s => s.Recipe)
            .WithMany(r => r.Steps) 
            .HasForeignKey(s => s.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Recipe)
            .WithMany(r => r.Comments)
            .HasForeignKey(s => s.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecipeVote>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeVotes)
            .HasForeignKey(ri => ri.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommentVote>()
            .HasOne(cv => cv.Comment)
            .WithMany(c => c.CommentVotes)
            .HasForeignKey(ci => ci.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        //many to many
        modelBuilder.Entity<RecipeTag>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeTags)
            .HasForeignKey(ri => ri.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Tag>().HasData(TagData.Data);
        modelBuilder.Entity<Recipe>().HasData(RecipeData.Recipe);
        modelBuilder.Entity<Step>().HasData(RecipeData.Step);
        modelBuilder.Entity<RecipeTag>().HasData(RecipeTagData.Data);
    }
}
