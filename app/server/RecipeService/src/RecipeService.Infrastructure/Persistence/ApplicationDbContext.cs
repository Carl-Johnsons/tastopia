using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeService.Domain.Entities;
using RecipeService.Infrastructure.Persistence.Mockup.Data;
using RecipeService.Infrastructure.Utilities;
using System.Runtime.CompilerServices;

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
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Vote> Votes { get; set; }
    //Relationship
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<RecipeTag> RecipeTags { get; set; }
    public DbSet<RecipeVote> RecipeVotes { get; set; }

    public DbSet<UserAllergicIngredient> UserAllergicIngredients { get; set; }
    public DbSet<UserBookmarkRecipe> UserBookmarkRecipes { get; set; }
    public DbSet<UserReportRecipe> UserReportRecipes { get; set; }
    public DbSet<UserViewRecipe> UserViewRecipes { get; set; }

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
        modelBuilder.Entity<Ingredient>(entity =>
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

        modelBuilder.Entity<Vote>(entity =>
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

        //many to many
        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeIngredients)
            .HasForeignKey(ri => ri.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecipeTag>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeTags)
            .HasForeignKey(ri => ri.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecipeVote>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeVotes)
            .HasForeignKey(ri => ri.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        //modelBuilder.Entity<Ingredient>().HasData(IngredientData.Data);
        //modelBuilder.Entity<Vote>().HasData(VoteData.Data);
        //modelBuilder.Entity<Tag>().HasData(TagData.Data);

        //RecipeData.Seed(modelBuilder);

        Recipes.Where(r => EF.Functions.JsonExists(r.Id, "key")).ToList();

    }
}
