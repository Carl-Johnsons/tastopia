using Contract.Interfaces;
using TrackingService.Domain.Entities;

namespace TrackingService.Domain.Interfaces;


public interface IApplicationDbContext : IMongoDbContext
{
    public DbSet<UserViewRecipeDetail> UserViewRecipeDetails { get; set; }

}
