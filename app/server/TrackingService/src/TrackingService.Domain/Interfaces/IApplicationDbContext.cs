using TrackingService.Domain.Entities;

namespace TrackingService.Domain.Interfaces;


public interface IApplicationDbContext : IDbContext
{
    public DbSet<UserViewRecipeDetail> UserViewRecipeDetails { get; set; }

}
