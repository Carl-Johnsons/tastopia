using Contract.Interfaces;
using TrackingService.Domain.Entities;

namespace TrackingService.Domain.Interfaces;


public interface IApplicationDbContext : IMongoDbContext
{
    public DbSet<UserViewRecipeDetail> UserViewRecipeDetails { get; set; }
    public DbSet<UserSearchRecipe> UserSearchRecipes { get; set; }
    public DbSet<UserSearchUser> UserSearchUsers { get; set; }
}
